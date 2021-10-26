using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.ViewModels
{
    class AdminViewModel : BaseViewModel
    {
        public AdminViewModel()
        {
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
            }
        }

        private string _Mail;
        public string Mail
        {
            get { return _Mail; }
            set
            {
                _Mail = value;
            }
        }

        private DateTime _Birthdate;
        public DateTime BirthDate
        {
            get { return _Birthdate; }
            set
            {
                _Birthdate = value;
            }
        }

        private int _UserType;
        public int UserType
        {
            get { return _UserType; }
            set
            {
                _UserType = value;
            }
        }
    }
}
