using Agency.PaidTimeOffDAL.Framework;
using System.Data.Linq.Mapping;

namespace Agency.PaidTimeOffDAL
{    
    public partial class ENTUserAccount : IENTBaseEntity { }

    public partial class ENTMenuItem : IENTBaseEntity { }

    public partial class ENTRole : IENTBaseEntity { }
    
    public partial class ENTCapability : IENTBaseEntity { }

    public partial class ENTRoleCapability : IENTBaseEntity { }

    public partial class ENTRoleUserAccount : IENTBaseEntity { }

    public partial class ENTWorkflow : IENTBaseEntity { }

    public partial class ENTWFOwnerGroup : IENTBaseEntity { }

    public partial class ENTWFOwnerGroupUserAccount : IENTBaseEntity 
    {
        private string _UserName;

        partial void OnUserNameChanging(string value);
        partial void OnUserNameChanged();

        [Column(Storage = "_UserName", DbType = "VarChar(102)", UpdateCheck = UpdateCheck.Never)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this.OnUserNameChanging(value);
                    this.SendPropertyChanging();
                    this._UserName = value;
                    this.SendPropertyChanged("UserName");
                    this.OnUserNameChanged();
                }
            }
        }    
    }

    public partial class ENTWFState : IENTBaseEntity { }

    public partial class ENTWFStateProperty : IENTBaseEntity { }

    public partial class ENTWFTransition : IENTBaseEntity 
    {
        private string _FromStateName;
        private string _ToStateName;

        partial void OnFromStateNameChanging(string value);
        partial void OnFromStateNameChanged();

        partial void OnToStateNameChanging(string value);
        partial void OnToStateNameChanged();        
                           
        [Column(Storage = "_FromStateName", DbType = "VarChar(50)", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public string FromStateName
        {
            get
            {
                return this._FromStateName;
            }
            set
            {
                if ((this._FromStateName != value))
                {
                    this.OnFromStateNameChanging(value);
                    this.SendPropertyChanging();
                    this._FromStateName = value;
                    this.SendPropertyChanged("FromStateName");
                    this.OnFromStateNameChanged();
                }
            }
        }

        [Column(Storage = "_ToStateName", DbType = "VarChar(50) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string ToStateName 
        {
            get
            {
                return this._ToStateName;
            }
            set
            {
                if ((this._ToStateName != value))
                {
                    this.OnToStateNameChanging(value);
                    this.SendPropertyChanging();
                    this._ToStateName = value;
                    this.SendPropertyChanged("ToStateName");
                    this.OnToStateNameChanged();
                }
            }
        }                
    }

    public partial class ENTWFItem : IENTBaseEntity 
    {
        private string _SubmitterUserName;

        partial void OnSubmitterUserNameChanging(string value);
        partial void OnSubmitterUserNameChanged();

        [Column(Storage = "_SubmitterUserName", DbType = "VarChar(102)", UpdateCheck = UpdateCheck.Never)]
        public string SubmitterUserName
        {
            get
            {
                return this._SubmitterUserName;
            }
            set
            {
                if ((this._SubmitterUserName != value))
                {
                    this.OnSubmitterUserNameChanging(value);
                    this.SendPropertyChanging();
                    this._SubmitterUserName = value;
                    this.SendPropertyChanged("SubmitterUserName");
                    this.OnSubmitterUserNameChanged();
                }
            }
        }
    }

    public partial class ENTWFItemOwner : IENTBaseEntity 
    {
        private string _UserName;

        partial void OnUserNameChanging(string value);
        partial void OnUserNameChanged();

        [Column(Storage = "_UserName", DbType = "VarChar(102)", UpdateCheck = UpdateCheck.Never)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this.OnUserNameChanging(value);
                    this.SendPropertyChanging();
                    this._UserName = value;
                    this.SendPropertyChanged("UserName");
                    this.OnUserNameChanged();
                }
            }
        }
    }

    public partial class ENTWFItemStateHistory : IENTBaseEntity 
    {
        private string _StateName;
        private string _OwnerName;
        private string _InsertedBy;

        partial void OnStateNameChanging(string value);
        partial void OnStateNameChanged();

        partial void OnOwnerNameChanging(string value);
        partial void OnOwnerNameChanged();

        partial void OnInsertedByChanging(string value);
        partial void OnInsertedByChanged();

        [Column(Storage = "_StateName", DbType = "VarChar(50)", UpdateCheck = UpdateCheck.Never)]
        public string StateName
        {
            get
            {
                return this._StateName;
            }
            set
            {
                if ((this._StateName != value))
                {
                    this.OnStateNameChanging(value);
                    this.SendPropertyChanging();
                    this._StateName = value;
                    this.SendPropertyChanged("StateName");
                    this.OnStateNameChanged();
                }
            }
        }

        [Column(Storage = "_OwnerName", DbType = "VarChar(102) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string OwnerName
        {
            get
            {
                return this._OwnerName;
            }
            set
            {
                if ((this._OwnerName != value))
                {
                    this.OnOwnerNameChanging(value);
                    this.SendPropertyChanging();
                    this._OwnerName = value;
                    this.SendPropertyChanged("OwnerName");
                    this.OnOwnerNameChanged();
                }
            }
        }

        [Column(Storage = "_InsertedBy", DbType = "VarChar(102) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string InsertedBy
        {
            get
            {
                return this._InsertedBy;
            }
            set
            {
                if ((this.InsertedBy != value))
                {
                    this.OnInsertedByChanging(value);
                    this.SendPropertyChanging();
                    this._InsertedBy = value;
                    this.SendPropertyChanged("InsertedBy");
                    this.OnInsertedByChanged();
                }
            }
        }                
    }

    public partial class PTORequest : IENTBaseEntity { }
    
    public partial class Holiday : IENTBaseEntity { }

    public partial class PTODayType : IENTBaseEntity { }

    public partial class PTORequestType : IENTBaseEntity { }

    public partial class PTOVacationBank : IENTBaseEntity 
    {
        private string _UserName;

        partial void OnUserNameChanging(string value);
        partial void OnUserNameChanged();

        [Column(Storage = "_UserName", DbType = "VarChar(102)", UpdateCheck = UpdateCheck.Never)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this.OnUserNameChanging(value);
                    this.SendPropertyChanging();
                    this._UserName = value;
                    this.SendPropertyChanged("UserName");
                    this.OnUserNameChanged();
                }
            }
        }
    }

    public partial class ENTEmail : IENTBaseEntity { }

    public partial class ENTNotification : IENTBaseEntity { }

    public partial class ENTNotificationENTUserAccount : IENTBaseEntity { }

    public partial class ENTNotificationENTWFState : IENTBaseEntity { }

    public partial class ENTReport : IENTBaseEntity { }

    public partial class ENTAuditObject : IENTBaseEntity { }

    public partial class ENTAuditObjectProperty : IENTBaseEntity { }

    public partial class ENTAudit : IENTBaseEntity { }

}
