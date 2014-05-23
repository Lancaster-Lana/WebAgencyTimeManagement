using System;
using System.Reflection;
using System.Transactions;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffBLL.Framework
{
    /// <summary>
    /// This class is the base for any BLL class that will perform insert, update, or delete actions on a table.
    /// </summary>    
    [Serializable]
    public abstract class ENTBaseEO : ENTBaseBO
    {        
        #region Members

        private ENTPropertyList _originalPropertyValues;

        #endregion Members

        #region Enumerations

        public enum DBActionEnum
        {
            Save,
            Delete
        }

        #endregion Enumerations

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ENTBaseEO() : base()
        {
            //Default the action to save.
            DBAction = DBActionEnum.Save;
        }

        #endregion Constructor

        #region Properties

        public DBActionEnum DBAction { get; set; }

        #endregion Properties

        #region Abstract Methods

        /// <summary>
        /// This method will add or update a record.
        /// </summary>
        public abstract bool Save(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId);

        /// <summary>
        /// This method validates the object's data before trying to save the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void Validate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors);

        /// <summary>
        /// This should call the business object's data class to delete the record.  The only method that should call this 
        /// is the virtual method "Delete(SqlTransaction tn, ref ValidationErrorAL validationErrors, int id)" in this class.
        /// </summary>
        protected abstract void DeleteForReal(HRPaidTimeOffDataContext db);

        /// <summary>
        /// This method validates the object's data before trying to delete the record.  If there is a validation error
        /// the validationErrors will be populated with the error message.
        /// </summary>
        protected abstract void ValidateDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors);

        /// <summary>
        /// This will load the object with the default properties.
        /// </summary>
        public abstract void Init();

        #endregion Abstratct Methods

        #region Protected\Public Methods

        public bool IsNewRecord()
        {
            return ID == 0;
        }

        /// <summary>
        /// This is used to save a record and start a new transaction.
        /// The implementor of BaseEO needs to create their own Save method that expects the 
        /// transaction to be passed in.
        /// </summary>
        public bool Save(ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Save)
            {
                // Begin database transaction
                using (var ts = new TransactionScope())
                {
                    // Create connection                    
                    using (var db = new HRPaidTimeOffDataContext())//DBHelper.GetHRPaidTimeOffConnectionString()))
                    {
                        //Now save the record
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            // Commit transaction if update was successful
                            ts.Complete();
                            return true;
                        }
                        return false;
                    }
                }
            }
            throw new Exception("DBAction not Save.");
        }

        /// <summary>
        /// This method will connect to the database and start a transaction.
        /// </summary>
        public bool Delete(ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                // Begin database transaction
                using (var ts = new TransactionScope())
                {
                    // Create connection
                    using (var db = new HRPaidTimeOffDataContext(DBHelper.GetHRPaidTimeOffConnectionString()))
                    {

                        //Delete the user
                        this.Delete(db, ref validationErrors, userAccountId);

                        if (validationErrors.Count == 0)
                        {
                            //Commit transaction since the delete was successful
                            ts.Complete();
                            return true;
                        }
                        //Rollback since the delete was not successful
                        return false;
                    }
                }
            }
            throw new Exception("DBAction not delete.");
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        internal virtual bool Delete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, int userAccountId)
        {
            if (DBAction == DBActionEnum.Delete)
            {
                //Check if this record can be deleted.  There may be referential integrity rules preventing it from being deleted                
                ValidateDelete(db, ref validationErrors);

                if (validationErrors.Count == 0)
                {
                    DeleteForReal(db);

                    //Chapter 12-Auditing
                    AuditDelete(db, ref validationErrors, userAccountId);
                    return true;
                }
                //The record can not be deleted.
                return false;
            }
            throw new Exception("DBAction not delete.");
        }

        protected void UpdateFailed(ref ENTValidationErrors validationErrors)
        {
            validationErrors.Add("This record was updated by someone else while you were editing it.  Your changes were not saved.  Click the Cancel button and enter this screen again to see the changes.");
        }

        #region Auditing

        protected void StorePropertyValues()
        {
            //Check if this object is being audited.
            var auditObject = new ENTAuditObjectEO();
            if (auditObject.Load(this.GetType().Name))
            {
                _originalPropertyValues = new ENTPropertyList();
                
                //Store the property values to an internal list
                //Create an instance of the type.            
                var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

                foreach (var auditObjectProperty in auditObject.Properties)
                {
                    //Name of property
                    _originalPropertyValues.Add(new ENTProperty
                    {
                        Name = auditObjectProperty.PropertyName,
                        Value = this.GetType().GetProperty(auditObjectProperty.PropertyName).GetValue(this, null)
                    });
                }                
            }
        }

        public void AuditAdd(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors, 
            int userAccountId)
        {
            //Check if the object is being audited
            var auditObject = new ENTAuditObjectEO();
            if (auditObject.Load(db, this.GetType().Name))
            {
                var audit = new ENTAuditEO();
                audit.ObjectName = this.GetType().Name;
                audit.RecordId = ID;
                audit.AuditType = ENTAuditEO.AuditTypeEnum.Add;
                audit.Save(db, ref validationErrors, userAccountId);
            }
        }

        public void AuditDelete(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors,
            int userAccountId)
        {
            var auditObject = new ENTAuditObjectEO();
            if (auditObject.Load(db, this.GetType().Name))
            {
                var audit = new ENTAuditEO();
                audit.ObjectName = this.GetType().Name;
                audit.RecordId = ID;
                audit.AuditType = ENTAuditEO.AuditTypeEnum.Delete;
                audit.Save(db, ref validationErrors, userAccountId);
            }
        }

        public void AuditUpdate(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors,
            int userAccountId)
        {
            foreach (var property in _originalPropertyValues)
            {
                object value = this.GetType().GetProperty(property.Name).GetValue(this, null);

                if (((value != null) && (property.Value != null)) &&
                    (Convert.ToString(value) != Convert.ToString(property.Value)))
                {
                    var audit = new ENTAuditEO();
                    audit.ObjectName = this.GetType().Name;
                    audit.RecordId = ID;
                    audit.PropertyName = property.Name;
                    audit.OldValue = (property.Value == null ? null : Convert.ToString(property.Value));
                    audit.NewValue = (Convert.ToString(value));
                    audit.AuditType = ENTAuditEO.AuditTypeEnum.Update;
                    audit.Save(db, ref validationErrors, userAccountId);
                }
            }
        }

        #endregion

        #endregion Protected Methods
    }
}
