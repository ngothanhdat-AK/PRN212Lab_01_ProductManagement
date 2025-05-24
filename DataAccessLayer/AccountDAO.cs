using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountID)
        {
            AccountMember accountMember = new AccountMember();
            if (accountID.Equals("PS0001"))
            {
                accountMember.MemberId = accountID;
                accountMember.MemberPassword = "123456";
                accountMember.MemberRole = 1;
            }
            return accountMember;
        }
    }
}
