using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NCBWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INCBService" in both code and config file together.
    [ServiceContract]
    public interface INCBService
    {
        [OperationContract]
        string Message();
        [OperationContract]
        bool Verify(string name, string cardNum);
        [OperationContract]
        bool VerifyFunds(float amount, string cardNum);
        [OperationContract]
        double CusFunds(string cardNum, double amnt);
    }

    [DataContract]
    public class getCardData
    {
        [DataMember]
        public DataTable Customer
        {
            get;
            set;
        }
    }
}