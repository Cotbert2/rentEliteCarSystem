﻿
using EntityLayer;

namespace BussinesLayer
{
    public class InsuranceBL
    {
        public EntityLayer.systemEntities.ResponseEL createInsurance(InsuranceEL insurance)
        {
            DataLayer.InsuranceDL insuranceDL = new DataLayer.InsuranceDL();
            return insuranceDL.createInsurance(insurance);
        }

        public InsuranceEL getInsuranceByBookingId(int bookingId)
        {
            DataLayer.InsuranceDL insuranceDL = new DataLayer.InsuranceDL();
            return insuranceDL.getInsuranceByBookingId(bookingId);
        }
    }
}
