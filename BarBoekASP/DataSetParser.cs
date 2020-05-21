using BarBoekASP.Data.MySQL;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP
{
    public class DataSetParser
    {
        public static AddressDTO DataSetToAddress(DataSet set, int rowIndex)
        {
            return new AddressDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                ZipCode = set.Tables[0].Rows[rowIndex][1].ToString(),
                Number = (int)set.Tables[0].Rows[rowIndex][2],
                Addition = set.Tables[0].Rows[rowIndex][3].ToString()
            };
        }

        public static ClubDTO DataSetToClub(DataSet set, int rowIndex)
        {
            AddressMySQLContext a;
            string connectionString = "Server=84.31.134.4;Database=barboekmain;User Id=newuser;Password=test;";
            a = new AddressMySQLContext(connectionString); //Zorgt ervoor dat er een daadwerkelijk adres wordt opgeslagen in ClubDTO
            return new ClubDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                Name = set.Tables[0].Rows[rowIndex][1].ToString(),
                Address = (AddressDTO)a.FindAddressById((int)set.Tables[0].Rows[rowIndex][2]),
                ClubNumber = set.Tables[0].Rows[rowIndex][3].ToString(),
                Email = set.Tables[0].Rows[rowIndex][4].ToString()


            };
        }

        public static MemberDTO DataSetToMember(DataSet set, int rowIndex)
        {
            AddressMySQLContext a;
            string connectionString = "Server=84.31.134.4;Database=barboekmain;User Id=newuser;Password=test;";
            a = new AddressMySQLContext(connectionString); //Zorgt ervoor dat er een daadwerkelijk adres wordt opgeslagen in MemberDTO
            return new MemberDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                Name = set.Tables[0].Rows[rowIndex][1].ToString(),
                Address = (AddressDTO)a.FindAddressById((int)set.Tables[0].Rows[rowIndex][2])

            };
        }

        public static PaymentDTO DataSetToPayment(DataSet set, int rowIndex)
        {
            return new PaymentDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                Succesful = (bool)set.Tables[0].Rows[rowIndex][1]
            };
        }

        public static ScheduleDTO DataSetToSchedule(DataSet set, int rowIndex)
        {
            return new ScheduleDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                Name = set.Tables[0].Rows[rowIndex][1].ToString(),
                VerenigingID = (int)set.Tables[0].Rows[rowIndex][2]
            };
        }

        public static ShiftDTO DataSetToShift(DataSet set, int rowIndex)
        {
            //YourEnum foo = (YourEnum)yourInt;
            return new ShiftDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                Name = set.Tables[0].Rows[rowIndex][1].ToString(),
                StartMoment = (DateTime)set.Tables[0].Rows[rowIndex][2],
                EndMoment = (DateTime)set.Tables[0].Rows[rowIndex][3],
                EventType = (ShiftDTO.Soort)(int)set.Tables[0].Rows[rowIndex][4],
                MaxMemberCount = (int)set.Tables[0].Rows[rowIndex][5]
            };
        }

    }
}
