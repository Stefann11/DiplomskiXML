using System.Data;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Adapter
{
    public class VerificationRequestAdapter : ITarget
    {
        private readonly VerificationRequestAdaptee verificationRequestAdaptee;

        public VerificationRequestAdapter(VerificationRequestAdaptee verificationRequestAdaptee)
        {
            this.verificationRequestAdaptee = verificationRequestAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return verificationRequestAdaptee.ConvertSqlDataReaderToVerificationRequest(dataRow);
        }
    }
}