using BarBoekASP.Data.MySQL;
using BarBoekASP.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                Address = (AddressDTO)a.FindAddressById((int)set.Tables[0].Rows[rowIndex][2]),
                BirthDate = (DateTime)set.Tables[0].Rows[rowIndex][3],
                Email = set.Tables[0].Rows[rowIndex][4].ToString(),
                Password = set.Tables[0].Rows[rowIndex][5].ToString(),
                Access = (MemberDTO.AccessLevel)set.Tables[0].Rows[rowIndex][6],
                BondNummer = (int)set.Tables[0].Rows[rowIndex][7],
                LastName = set.Tables[0].Rows[rowIndex][8].ToString(),
                Initials = set.Tables[0].Rows[rowIndex][9].ToString(),
                Insertion = set.Tables[0].Rows[rowIndex][10].ToString(),
                PhoneNumber = set.Tables[0].Rows[rowIndex][9].ToString(),
                Gender = set.Tables[0].Rows[rowIndex][10].ToString(),
                PhoneWork = set.Tables[0].Rows[rowIndex][11].ToString(),
                PhoneMobile = set.Tables[0].Rows[rowIndex][12].ToString()


            };
        }

        public static PaymentDTO DataSetToPayment(DataSet set, int rowIndex)
        {
            return new PaymentDTO()
            {
                ID = (int)set.Tables[0].Rows[rowIndex][0],
                MemberShiftID = (int)set.Tables[0].Rows[rowIndex][1],
                Succesful = (bool)set.Tables[0].Rows[rowIndex][2]
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
                EventType = (Soort)(int)set.Tables[0].Rows[rowIndex][4],
                MaxMemberCount = (int)set.Tables[0].Rows[rowIndex][5]
            };
        }

        public static ShiftDTO DataSetToShiftWithMember(DataSet shift,DataSet member, int rowIndex)
        {
            //MemberMySQLContext a;
            //ShiftMySQLContext b;
            //string connectionString = "Server=84.31.134.4;Database=barboekmain;User Id=newuser;Password=test;";
            //a = new MemberMySQLContext(connectionString);
            //b = new ShiftMySQLContext(connectionString);

            ShiftDTO output = DataSetParser.DataSetToShift(shift, 0);
            output.Members = DataSetParser.DataSetToMember(member, 0);

            //return new ShiftDTO()
            //{
            //    Members = DataSetParser.DataSetToMember(member, 0),

            //};
            return output;
        }

    }
}
