using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

namespace appSchool.Repositories
{
    public class SendMessegeRepository : GenericRepository<vStudentSession>
    {
        public SendMessegeRepository() : base(new dbSchoolAppEntities()) { }
        public SendMessegeRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        const string NorthwindDataContextKey = "DXNorthwindDataContext";

        public static dbSchoolAppEntities DB
        {
            get
            {
               
                return (dbSchoolAppEntities)HttpContext.Current.Items[NorthwindDataContextKey];
            }
        }

        public List<vStudentSession> GetStudentSessionForGrid()
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

            List<vStudentSession> obj1 = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where EnableSMSFeature=1").ToList();
            return obj1;
        }

        public List<vStudentSession> GetStudentSessionByClassSetupIDs(string mClassSetupID,int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentSession> obj = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in ("+mClassSetupID +") and TCGiven=0  and SessionID="+mSessionID+" AND CompID="+mCompID+" AND BranchID="+ mBranchID +" ").ToList();
            return obj;
        }

        public List<vStudentSession> GetStudentSessionDefaultListByClassSetupIDs()
        {
            List<vStudentSession> list = new List<vStudentSession>();
            //List<vStudentSession> obj = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and TCGiven=0  and SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
            return list;
        }

        public PersonalContactList GetContactListByID(int mPersonalPersonID, byte mCompID, byte mBranchID)
        {
            PersonalContactList obj = new PersonalContactList();
            obj = this.context.PersonalContactLists.Where(x => x.PersonalPersonID == mPersonalPersonID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();

            return obj;
        }

        public List<PersonalContactList> GetAllPersonalContactListForSendingMessage(int mSessionID, byte mCompID, byte mBranchID)
        {
            string sql = " Select * From PersonalContactList ";


            List<PersonalContactList> obj = this.context.PersonalContactLists.SqlQuery(sql).ToList();
            return obj;
        }

        public List<vStudentSession> GetStudentSessionByClassNReligions(string mClassSetupID, string mReligion, int mSessionID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();
            string[] mlist = mReligion.Split(',');
            string mlistReligion = string.Empty;
            for (int i = 0; i <= mlist.Length - 1; i++)
            {
                mlistReligion += "'" + mlist[i] + "',";
            }
            mlistReligion = mlistReligion.Substring(0, mlistReligion.Length - 1);
            List<vStudentSession> obj1 = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") AND TCGiven=0 and Religion in (" + mlistReligion + ") and SessionID=" + mSessionID + "").ToList();
            return obj1;
        }

        public List<vStudentSession> GetStudentSessionByClassNHouse(string mClassSetupID, string mHouse, int mSessionID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();
            string[] mlist = mHouse.Split(',');
            string mlistHouse = string.Empty;
            for (int i = 0; i <= mlist.Length - 1; i++)
            {
                mlistHouse += "" + mlist[i] + ",";
            }
            mlistHouse = mlistHouse.Substring(0, mlistHouse.Length - 1);
            List<vStudentSession> objvStudentSession = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") AND TCGiven=0 and HouseID in (" + mlistHouse + ") and SessionID=" + mSessionID + " ").ToList();
            return objvStudentSession;
        }



        public List<VStudentDetail> GetStudentListforSMSWishes(int mWishTypeID, int mSessionID, byte mCompID, byte mBranchID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();
            List<VStudentDetail> objstud = new List<VStudentDetail>();
            switch (mWishTypeID)
            {
                case 1:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(DateOfBirth)= MONTH(GetDate()) and DAY(DateOfBirth)=DAY(GetDate()) AND SessionID="+mSessionID + " AND CompID="+mCompID+" AND BranchID="+mBranchID ).ToList();
                    break;
                case 2:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(FatherDOB)= MONTH(GetDate()) and DAY(FatherDOB)=DAY(GetDate()) AND SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID).ToList();
                    break;
                case 3:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(MotherDOB)= MONTH(GetDate()) and DAY(MotherDOB)=DAY(GetDate())AND SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID).ToList();
                    break;
                case 4:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(AnniversaryDate)= MONTH(GetDate()) and DAY(AnniversaryDate)=DAY(GetDate())AND SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID).ToList();
                    break;



            }


            // List<vStudentSession> obj1 = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "").ToList();
            return objstud;
        }

        public List<vStudentLoginDetail> GetStudentForSendingUserIDPassword(int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentLoginDetail> obj = new List<vStudentLoginDetail>();
            obj = this.context.vStudentLoginDetails.Where(x => x.StudentID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vParentLoginDetail> GetStudentForSendingParentIDPassword(int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vParentLoginDetail> obj = new List<vParentLoginDetail>();
            obj = this.context.vParentLoginDetails.Where(x => x.StudentID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vTeacherLoginDetail> GetStudentForSendingTeacherIDPassword(int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vTeacherLoginDetail> obj = new List<vTeacherLoginDetail>();
            obj = this.context.vTeacherLoginDetails.Where(x => x.TeacherID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }




        public List<VStudentDetail> GetStudentListforSchedulerWishes(int mWishTypeID, int mSessionID, DateTime mFromDate, DateTime mToDate, byte mCompID, byte mBranchID)
        {
          
            List<VStudentDetail> objstud = new List<VStudentDetail>();

            string sql = "SELECT * FROM dbo.VStudentDetail where MONTH(DateOfBirth)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and ( DAY(DateOfBirth)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(DateOfBirth)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "')  AND CompID="+ mCompID +" AND BranchID="+mBranchID+" AND SessionID="+mSessionID+" ";

            switch (mWishTypeID)
            {
                case 1:
                    //objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where (MONTH(DateOfBirth)>=MONTH(" + mFromDate.ToShortDateString() + ") and   MONTH(DateOfBirth)<=MONTH(" + mToDate.ToShortDateString() + ") ) or  (  DAY(DateOfBirth)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(DateOfBirth)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(DateOfBirth)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and ( DAY(DateOfBirth)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(DateOfBirth)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )    AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND SessionID=" + mSessionID + "   ").ToList();
                    break;
                case 2:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where  MONTH(FatherDOB)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and ( DAY(FatherDOB)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(FatherDOB)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "AND SessionID=" + mSessionID + "   ").ToList();
                    //objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where (MONTH(FatherDOB)>=MONTH(" + mFromDate.ToShortDateString() + ") and   MONTH(FatherDOB)<=MONTH(" + mToDate.ToShortDateString() + ") ) or  (  DAY(FatherDOB)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(FatherDOB)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    break;
                case 3:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(MotherDOB)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and (DAY(MotherDOB)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(MotherDOB)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND SessionID=" + mSessionID + " ").ToList();
                    //objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where (MONTH(MotherDOB)>=MONTH(" + mFromDate.ToShortDateString() + ") and   MONTH(MotherDOB)<=MONTH(" + mToDate.ToShortDateString() + ") ) or  (  DAY(MotherDOB)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(MotherDOB)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    break;
                case 4:
                    objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where MONTH(AnniversaryDate)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and (DAY(AnniversaryDate)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(AnniversaryDate)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND SessionID=" + mSessionID + " ").ToList();
                    //objstud = this.context.VStudentDetails.SqlQuery("SELECT * FROM dbo.VStudentDetail where (MONTH(AnniversaryDate)>=MONTH(" + mFromDate.ToShortDateString() + ") and   MONTH(AnniversaryDate)<=MONTH(" + mToDate.ToShortDateString() + ") ) or  (  DAY(AnniversaryDate)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(AnniversaryDate)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    break;



            }


            // List<vStudentSession> obj1 = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "").ToList();
            return objstud;
        }


        public List<Teacher> GetTeacherListforSMSWishes(int mWishTypeID, int mSessionID, byte mCompID, byte mBranchID )
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();
            List<Teacher> objteacher = new List<Teacher>();
            switch (mWishTypeID)
            {
                case 1:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where MONTH(DOB)= MONTH(GetDate()) and DAY(DOB)=DAY(GetDate())  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "   ").ToList();
                    break;
                case 2:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where MONTH(AnniversaryDate)= MONTH(GetDate()) and DAY(AnniversaryDate)=DAY(GetDate())  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "  ").ToList();
                    break;
                case 3:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where TeacherID>0   AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "  ").ToList();
                    break;
            }

            return objteacher;
        }

        public List<Teacher> GetTeacherListforSchedulerWishes(int mWishTypeID, int mSessionID, DateTime mFromDate, DateTime mToDate, byte mCompID, byte mBranchID )
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();
            List<Teacher> objteacher = new List<Teacher>();

          
            switch (mWishTypeID)
            {
                case 1:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where MONTH(DOB)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and (DAY(DOB)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(DOB)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )   AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "   ").ToList();
                    //objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where ( MONTH(DOB)>= MONTH(" + mFromDate.ToShortDateString() + ") And MONTH(DOB)<= MONTH(" + mToDate.ToShortDateString() + ") ) or ( DAY(DOB)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(DOB)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    break;
                case 2:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where MONTH(AnniversaryDate)=MONTH('" + mFromDate.ToString("MM/dd/yyyy") + "') and (DAY(AnniversaryDate)>=DAY('" + mFromDate.ToString("MM/dd/yyyy") + "') AND  DAY(AnniversaryDate)<=DAY('" + mToDate.ToString("MM/dd/yyyy") + "') )  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + "  ").ToList();
                    //objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where ( MONTH(AnniversaryDate)>= MONTH(" + mFromDate.ToShortDateString() + ") And MONTH(AnniversaryDate)<= MONTH(" + mToDate.ToShortDateString() + ") ) or ( DAY(AnniversaryDate)>=DAY(" + mFromDate.ToShortDateString() + ") AND  DAY(AnniversaryDate)<=DAY(" + mToDate.ToShortDateString() + ") ) ").ToList();
                    break;
                case 3:
                    objteacher = this.context.Teachers.SqlQuery("SELECT (FirstName + ' '+ isnull(LastName,0)) as FirstName ,* FROM dbo.Teacher where TeacherID>0   AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
                   
                    break;
            }

            return objteacher;
        }


        public vStudentSession GetStudentForSendingSMSbyStudentID(int mStudentID, int mSessionID, byte mCompID, byte mBranchID)
        {
            vStudentSession objvSS = new vStudentSession();
            objvSS = this.context.vStudentSessions.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID ).SingleOrDefault();
            return objvSS;
        }


        public vStudentLoginDetail GetStudentForSendingLoginIDPasswordbyStudentID(int mStudentID, int mSessionID, byte mCompID, byte mBranchID)
        {
            vStudentLoginDetail objvSS = new vStudentLoginDetail();
            objvSS = this.context.vStudentLoginDetails.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return objvSS;
        }


        public vParentLoginDetail GetParentForSendingParentLoginIDPasswordbyStudentID(int mStudentID, int mSessionID, byte mCompID, byte mBranchID)
        {
            vParentLoginDetail objvSS = new vParentLoginDetail();
            objvSS = this.context.vParentLoginDetails.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return objvSS;
        }


        public vTeacherLoginDetail GetTeacherForSendingTeacherLoginIDPasswordbyTeacherID(int mTeacherID, int mSessionID, byte mCompID, byte mBranchID)
        {
            vTeacherLoginDetail objvSS = new vTeacherLoginDetail();
            objvSS = this.context.vTeacherLoginDetails.Where(x => x.TeacherID == mTeacherID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return objvSS;
        }

        public List<vStudentLoginDetail> GetStudentForSendingUserIDPasswordByClassSetupID(string mClassSetupID, int mSessionID, byte mCompID, byte mBranchID)
        {
            //List<vStudentLoginDetail> obj = new List<vStudentLoginDetail>();
            //obj = this.context.vStudentLoginDetails.Where(x => x.StudentID > 0 && x.ClassSetupID == mClassSetupID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            List<vStudentLoginDetail> obj = this.context.vStudentLoginDetails.SqlQuery("SELECT * FROM dbo.vStudentLoginDetail where ClassSetupID in (" + mClassSetupID + ")  and SessionID=" + mSessionID + " AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
            return obj;

        }
        public vTeacherLoginDetail GetTeacherForSendingTeacherLoginIDPasswordbyTeacherID(int mTeacherID, byte mCompID, byte mBranchID)
        {
            vTeacherLoginDetail objvSS = new vTeacherLoginDetail();
            objvSS = this.context.vTeacherLoginDetails.Where(x => x.TeacherID == mTeacherID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return objvSS;
        }


        public void SaveSmsLog(string msg, int sentby, int orderedby, byte sessionid, byte mCompID, byte mBranchID)
        {
            SMSLogMaster obj = new SMSLogMaster();
            obj.SMSText = msg;
            obj.SessionID = sessionid;
            obj.OrderBy = orderedby;
            obj.SentDateTime = System.DateTime.Now;
            obj.CompID = mCompID;
            obj.BranchID = mBranchID;

            this.context.SMSLogMasters.Add(obj);
            this.context.SaveChanges();
        }

        public string SendMesssageText(string mMsg, string mMob, byte mCompID)
        {
            string ReturnMsg = string.Empty;

            if (mMob != null)
            {
                if (mMsg.Trim() != string.Empty && mMsg.Trim() != "" && mMsg.Trim() != null && mMob.Trim() != string.Empty && mMob.Trim() != "" && mMob.Trim() != null)
                {

                    string[] LstMob = mMob.Split(',');
                    string lst = string.Empty;
                    if (LstMob.Length > 100)
                    {
                        int m = 0, n = 99;


                        for (int i = 0; i <= LstMob.Length / 100; i++)
                        {
                            lst = string.Empty;
                            for (int j = m; j <= n; j++)
                            {
                                lst += LstMob[j] + ",";

                            }

                            lst = lst.TrimEnd(',');
                            m += 100;
                            if (n + 100 > LstMob.Length)
                                n = LstMob.Length - 1;
                            else
                                n += 100;

                            string mApi = string.Empty;

                            switch (byte.Parse(mCompID.ToString()))
                            {
                                //------- 1. Nirmala Convent--------------new api 16/04/2021
                                case 1:
                                    //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=31";
                                    break;
                                //----------------ST Mark-----------------
                                case 2:
                                //    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=31";
                                    break;
                                //--------------KVJ------------------
                                case 3:
                                    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                                    break;
                                //--------------St Xavier------------------
                                case 4:
                                    //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number="+lst+"&text=" + mMsg + "&route=31";
                                    break;

                                case 5:
                                    //-- St Joseph--//
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                    break;
                                case 6:
                                    //-- St lawrence--//
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                    break;
                                case 7:
                                    //-- Shanti Dham-//
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                    break;
                            }



                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                            string responseString = respStreamReader.ReadToEnd().ToString();

                            //ReturnMsg = responseString;

                            ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                            respStreamReader.Close();
                            myResp.Close();
                        }
                    }
                    else
                    {
                        string mApi = string.Empty;
                        switch (byte.Parse(mCompID.ToString()))
                        {
                            //------- 1. Nirmala Convent--------------
                            case 1:
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=91"+mMob+"&text=" + mMsg + "&route=31";
                                //mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=366191e0-6818-4746-a728-477e4c1b738a&senderid=STmark&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";
                                
                                break;
                            case 2:
                               // mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";
                                break;
                            case 3:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                                break;
                            //--------------St Xavier------------------
                            case 4:
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";
                                break;
                            case 5:
                                //-- St Joseph--//
                                mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + mMob + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2";
                                break;
                            case 6:
                                //-- St lawrence--//
                                mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";

                                break;
                            case 7:
                                //-- Shanti Dham-//
                                mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";
                                break;
                        }


                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd().ToString();


                        //ReturnMsg = responseString;
                        ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString ;

                        respStreamReader.Close();
                        myResp.Close();

                    }

                }
                else
                {
                    ReturnMsg += " Message field is blank.";
                }
            }
            
            return ReturnMsg;
        }

        public string SendMesssageTextHindi(string mMsg, string mMob, byte mCompID)
        {
            string ReturnMsg = string.Empty;
            if (mMob != null)
            {
                if (mMsg.Trim() != string.Empty && mMsg.Trim() != "" && mMsg.Trim() != null && mMob.Trim() != string.Empty && mMob.Trim() != "" && mMob.Trim() != null)
                {

                    string[] LstMob = mMob.Split(',');
                    string lst = string.Empty;
                    if (LstMob.Length > 100)
                    {
                        int m = 0, n = 99;


                        for (int i = 0; i <= LstMob.Length / 100; i++)
                        {
                            lst = string.Empty;
                            for (int j = m; j <= n; j++)
                            {
                                lst += LstMob[j] + ",";
                            }

                            lst = lst.TrimEnd(',');
                            m += 100;
                            if (n + 100 > LstMob.Length)
                                n = LstMob.Length - 1;
                            else
                                n += 100;

                            string mApi = string.Empty;
                            switch (byte.Parse(mCompID.ToString()))
                            {
                                //------- 1. Nirmala Convent--------------
                                case 1:
                                    //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number="+lst+"&text=" + mMsg + "&route=1";
                                    break;
                                case 2:
                                   // mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=1";
                                    break;
                                case 3:
                                    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    break;
                                //--------------St Xavier------------------
                                case 4:
                                    //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=1";
                                    break;

                                case 5:
                                    //-- St Joseph--////Id-joseph1////pwd-497993
                                   mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + lst + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    
                                    break;
                                case 6:
                                    //-- St Lawrence--
                                    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=slsmeh&pwd=slsmeh@123&to=" + lst + "&sid=SLSMEH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    break;
                                case 7:
                                    //-- Shanti Dham--
                                
                                    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + lst + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                    break;
                            }

                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                            string responseString = respStreamReader.ReadToEnd().ToString();

                            // ReturnMsg = responseString;
                            ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                            respStreamReader.Close();
                            myResp.Close();
                        }

                    }
                    else
                    {
                        string mApi = string.Empty;
                        switch (byte.Parse(mCompID.ToString()))
                        {
                            //------- 1. Nirmala Convent--------------
                            case 1:
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number=91"+mMob+"&text="+mMsg+"&route=1";
                                break;
                            case 2:
                                //------- 2 StMark--------------
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=1";
                                break;
                            case 3:
                                //------- 3 KVJII--------------
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                break;
                            //--------------St Xavier------------------
                            case 4:
                                //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=1";
                                break;
                            //--------------St Joseph--////Id-joseph1////pwd-497993
                            case 5:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + mMob + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                break;
                            case 7:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + mMob + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                break;

                        }

                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd().ToString();
                        //ReturnMsg = responseString;
                        ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                        respStreamReader.Close();
                        myResp.Close();

                    }
                }
                else
                {
                    ReturnMsg += " Message field is blank.";
                }
            }

          
            return ReturnMsg;
        }


        public string SendMesssageText(string mMsg, string mMob, DateTime mDt, byte mCompID)
        {
            string ReturnMsg = string.Empty;
            string[] LstMob = mMob.Split(',');
            string lst = string.Empty;
            if (LstMob.Length > 100)
            {
                int m = 0, n = 99;


                for (int i = 0; i <= LstMob.Length / 100; i++)
                {
                    lst = string.Empty;
                    for (int j = m; j <= n; j++)
                    {
                        lst += LstMob[j] + ",";
                    }

                    lst = lst.TrimEnd(',');
                    m += 100;
                    if (n + 100 > LstMob.Length)
                        n = LstMob.Length - 1;
                    else
                        n += 100;

                    string mApi = string.Empty;
                    switch (mCompID)
                    {
                        //------- 1. Nirmala Convent--------------
                        case 1:
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 2:
                            //---old Api---
                            ///mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=366191e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 3:
                           //------New Api------26-09-2016
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=f4de4b58-9290-4c84-bfc8-6357b4049b79&senderid=KVJIII&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 4:
                        //------New Api------//------New Api------26-09-2016
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 5:
                             //------New Api------//------New Api------26-09-2016
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 6:
                            
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 7:
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;


                    }



                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                     ReturnMsg = responseString;
                    //ReturnMsg = "Message Sent Successfully";
                }
            }
            else
            {
                string mApi = string.Empty;
                switch (mCompID)
                {
                    //------- 1. Nirmala Convent--------------
                    case 1:
                         mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 2:
                        ///----Old Api--------///
                        //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 3:
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=f4de4b58-9290-4c84-bfc8-6357b4049b79&senderid=KVJIII&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 4:
                        //------New Api------//------New Api------26-09-2016
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 5:
                        //------New Api------//------New Api------26-09-2016
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 6:
                        //------New Api------//------New Api------26-09-2016
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 7:
                        //------New Api------//------New Api------19-06-2019
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                }



                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                 ReturnMsg = responseString;
                //ReturnMsg = "Message Sent Successfully";
            }

            return ReturnMsg;
        }


        public string SendMesssageTextHindi(string mMsg, string mMob, DateTime mDt, byte mCompID)
        {
            string ReturnMsg = string.Empty;
            string[] LstMob = mMob.Split(',');
            string lst = string.Empty;
            if (LstMob.Length > 100)
            {
                int m = 0, n = 99;


                for (int i = 0; i <= LstMob.Length / 100; i++)
                {
                    lst = string.Empty;
                    for (int j = m; j <= n; j++)
                    {
                        lst += LstMob[j] + ",";
                    }
                    lst = lst.TrimEnd(',');
                    m += 100;
                    if (n + 100 > LstMob.Length)
                        n = LstMob.Length - 1;
                    else
                        n += 100;

                    string mApi = string.Empty;
                    switch (mCompID)
                    {
                        //------- 1. Nirmala Convent--------------
                        case 1:
                            
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 2:
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=366191e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 3:
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=f4de4b58-9290-4c84-bfc8-6357b4049b79&senderid=KVJIII&channel=2&DCS=8&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                            break;
                        case 4:
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                            break;
                        case 5:
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + lst + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=8&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                            break;
                        case 6:
                            mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=slsmeh&pwd=slsmeh@123&to=" + lst + "&sid=SLSMEH&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            break;
                        case 7:
                            //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + lst + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                            mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=8&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                            break;
                    }



                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                     ReturnMsg = responseString;
                    //ReturnMsg = "Message Sent Successfully";
                }

            }
            else
            {
                string mApi = string.Empty;
                switch (mCompID)
                {
                    //------- 1. Nirmala Convent--------------
                    //------- 1. Nirmala Convent--------------
                    case 1:
                        //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 2:
                        //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=" + mMob + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=clickhere";
                        break;
                    case 3:
                        mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                        break;
                    case 4:
                        //mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS@eee&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                        mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt") + "&route=31";
                       break;
                    case 5:
                       mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + mMob + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                      break;
                    case 6:
                      mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=slsmeh&pwd=slsmeh@123&to=" + mMob + "&sid=SLSMEH&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                      break;
                    case 7:
                      mApi = "http://www.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + lst + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8&schedtime=" + mDt.ToString("yyyy/MM/dd hh:mm:ss tt");
                      break;
                }




                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                ReturnMsg = responseString;
                //ReturnMsg = "Message Sent Successfully";

            }
            return ReturnMsg;

        }
        

    }

    public class modelSendSms
    {
        public modelSendSms()
        {
            PrefixEnglish = string.Empty;
            PrefixHindi = string.Empty;
            smsTextEnglish = string.Empty;
            smsTextHindi = string.Empty;
            smsMobileNo = string.Empty;
            smsStudentID = string.Empty;
            OrderedBy = 0;
            smsCopy = false;
            smsAdminCopy = false;
            includeName = true;
            SMSLanguage = 0;
            TemplateType = 0;
            

        }

        public string PrefixEnglish { get; set; }
        public string PrefixHindi { get; set; }
        public string  smsTextEnglish { get; set; }
        public string smsTextHindi { get; set; }
        public string smsMobileNo { get; set; }
        public string smsStudentID { get; set; }
        public int OrderedBy { get; set; }
        public bool smsCopy { get; set; }
        public bool smsAdminCopy { get; set; }
        public bool includeName { get; set; }
        public int SMSLanguage { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int TemplateType { get; set; }
    }


    public class modelEmployeeSendSms
    {
        public modelEmployeeSendSms()
        {
            PrefixEnglish = string.Empty;
            PrefixHindi = string.Empty;
            smsTextEnglish = string.Empty;
            smsTextHindi = string.Empty;
            smsMobileNo = string.Empty;
            smsTeacherID = string.Empty;
            OrderedBy = 0;
            smsCopy = false;
            includeName = true;
            SMSLanguage = 0;
            TemplateType = 0;


        }

        public string PrefixEnglish { get; set; }
        public string PrefixHindi { get; set; }
        public string smsTextEnglish { get; set; }
        public string smsTextHindi { get; set; }
        public string smsMobileNo { get; set; }
        public string smsTeacherID { get; set; }
        public int OrderedBy { get; set; }
        public bool smsCopy { get; set; }
        public bool includeName { get; set; }
        public int SMSLanguage { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int TemplateType { get; set; }
    }


    public class modelListSmsReport
    {
        public enum msgStatus
        {
            InQueue = 0,
            Submitted = 1,
            UnDelivered = 2,
            Delivered = 3,
            Expired = 4,
            Rejected = 8,
            Sent = 9,
            DND = 10,
            InvalidNo = 11
        }
        public string MobileNumber { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public string SubmitDate { get; set; }
        public string DeliveryDate { get; set; }
        public string  MessageStatus { get; set; }
        public string AliasMessageId { get; set; }

    }

    public class JsonResultSMS
    {
        //public string id { get; set; }
        //public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<ResultSMS> Reports { get; set; }
    }

    //public class ResultSMS
    //{
    //    public string id { get; set; }
    //    public string UniqueId { get; set; }
    //    public string CampaignId { get; set; }
    //    public string Message { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string SubmitDate { get; set; }
    //    public string SenderId { get; set; }
    //    public string SiteUserId { get; set; }
    //    public string Udh { get; set; }
    //    public string MessageStatus { get; set; }
    //    public string Route { get; set; }
    //    public string ESMClass { get; set; }
    //    public string DataCoding { get; set; }
    //    public string AliasMessageId { get; set; }
    //    public string IsTransactional { get; set; }
    //    public string Priority { get; set; }
    //    public string IsSMPP { get; set; }
       
    //}


    public class ResultSMS
    {

        public string DateTime { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string MessageId { get; set; }
        public string SenderId { get; set; }
        public string Type { get; set; }

       //public string MobileNumber { get; set; }
       //public string     SenderId { get; set; }
       //public string  Message { get; set; }
       //public string  SubmitDate { get; set; }
       //public string  MessageStatus { get; set; }
       //public string   DeliveryDate { get; set; }
       //public string   AliasMessageId { get; set; }
       //public string Type { get; set; }

       
    }
    
}