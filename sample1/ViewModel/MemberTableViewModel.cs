using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class MemberTableViewModel : ViewModelBase
    {
        public readonly User _member;
        public string MemberID => _member.ID;
        public string MemberName => $"{_member.FirstName} {_member.MiddleName} {_member.LastName}";
        public string MemberUsername => _member.Username;
        public int MemberPenalty => _member.Reputation;

        public MemberTableViewModel(User member)
        {
            _member = member;
        }
    }
}
