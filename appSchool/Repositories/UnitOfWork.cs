using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.Repositories;
namespace appSchool.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private dbSchoolAppEntities context = new dbSchoolAppEntities();

        private ClassRepository classRepository;
        private SectionRepository sectionRepository;
        private SMSTypeRepository smsTypeRepository;
        private FeesCategoryRepository feesCategoryRepository;
        private HouseRepository houseRepository;
        private ClassCategoryRepository classCategoryRepository;
        private ExamCategoryRepository examCategoryRepository;
        private SMSTemplateRepository smsTemplatesRepository;
       
        private ClassSetupRepository classSetupRepository;
        private AccountMasterRepository accountMasterRepository;
      
        private AcademicyearRepository academicYearRepository;
        private BusMasterRepository busMasterRepository;
        private DepartmentMasterRepository departmentMasterRepository;
        private DesignationMasterRepository designationMasterRepository;
        private BusStopMasterRepository busStopMasterRepository;
        private RouteMasterRepository routeMasterRepository;
        
        private GradeMasterRepository gradeMasterRepository;
        private TeacherRegistrationRepository teacherRegistrationRepository;
        private InstalmentMasterRepository instalmentMasterRepository;
        private OwnerMasterRepository ownerMasterRepository;
        private StudentRegistrationRepository studentRegistrationRepository;
        private StudentSessionRepository studentSessionRepository;
        private AttendanceClassRepository attendanceClassRepository;
        private AttendanceStudentRepository attendanceStudentRepository;
        private SendMessegeRepository sendMessegeRepository;
        private FeesHeadRepository feesHeadRepository;
        private FeeTermRepository feeTermRepository;
        private UserLoginRepository userLoginRepository;
        private FeesStructureMasterRepository feesStructureMasterRepository;
        private FeesStructureDetailRepository feesStructureDetailRepository;
        private vListFeesStructureRepository vlistFeesStructureRepository;
        private SchoolMasterRepository schoolMasterRepository;
        private StudentFeesMasterRepository studentFeesMasterRepository;
        private StudentFeesDetailRepository studentFeesDetailRepository;
        private VStudentDetailRepository VstudentDetailRepository;
        private FeesCollectionMasterRepository feesCollectionMasterRepository;
        private FeesCollectionDetailRepository feesCollectionDetailRepository;
        private FeesReceiptRepository feesReceiptRepository;
        private SetupRepository setupRepository;
        private vStudentDataExportRepository VStudentDataExportRepository;
        private vTeacherDataExportDateWiseRepository vTeacherDataExportDateWiseRepository;
        private vTeacherDataExportRepository vteacherDataExportRepository;
        private vAttendanceDataExportRepository vattendanceDataExportRepository;
        private ApplicationImageRepository applicationImageRepository;
        private vFeesStructureDataExportRepository vfeesStructureDataExportRepository;
        private NewsEventMasterRepository newsEventMasterRepository;
        private PhotoGalleryMasterRepository photoGalleryMasterRepository;

        private vStudentFeesStructDataExportRepository vstudentFeesStructDataExportRepository;
        private vAttendanceDailyChartRepository vattendanceDailyChartRepository;
        private vStudentStrengthChartRepository vstudentStrengthChartRepository;
        private vStudentBirthdayRepository vstudentBirthdayRepository;
        private vSessionAdmissionChartRepository vsessionAdmissionChartRepository;
        private StudentParentLoginRepository studentParentLoginRepository;
        private vFeeSummaryQuaterwiseChartRepository vfeeSummaryQuaterwiseChartRepository;
        private NoticeBoardRepository noticeBoardRepository;
        private VStudentFeesReminderRepository VstudentFeesReminderRepository;
        private UserPermissionRepository userPermissionRepository;
        private vSMSLogDataExportRepository vsmsLogDataExportRepository;
        private SettingMasterRepository settingMasterRepository;
        private ContectListRepository contectListRepository;
        private StudentDocumentRepository studentDocumentRepository;
        private TeacherAchivmentRepository teacherAchivmentrepository;
        private SubjectExpertiseRepository subjectExpertieserepository;
        private SubjectLevel1Repository subjectLevelrepository;
        private TeacherExpertiseRepository teacherExpertiserepository;
        private TeacherQualificationRepository teacherQualificationrepository;
        private StudentPriviousDetailsRepository studentPriviousdetailrepository;
        private vStudentListFromStudFeeStructureRepository vstudentListFromStudFeesStructurerepository;
        private ShelfMasterRepository shelfMasterRepository;
        private RackMasterRepository rackMasterRepository;
        private ClassSyllabusMasterRepository classSyllabusMasterRepository;
        private ClassSyllabusDetailRepository classSyllabusDetailRepository;
        private SubjectLevel2Repository subjectLevel2repository;
        private SubjectLevel3Repository subjectLevel3repository;
        private TimeScheduleMasterRepository timeScheduleMasterRepository;
        private FacultyAllotmentMasterRepository facultyAllotmentMasterRepository;
        private FacultyAllotmentDetailRepository facultyAllotmentDetailRepository;
        private ExamRemarkMasterRepository examRemarkMasterRepository;
        private ExamSyllabusDetailRepository examSyllabusDetailRepository;
        private ExamSyllabusMasterRepository examSyllabusMasterRepository;
        private ExamSetupMasterRepository examSetupMasterRepository;
        private ExamSetupDetailRepository examSetupDetailRepository;
        private RouteDetailRepository routeDetailRepository;
        private BusRoutePlanRepository busRoutePlanRepository;
        private DriverMasterRepository driverMasterRepository;
        private AdmissionFormSaleRepository admissionFormSaleRepository;
        private InsuranceDetailRepository insuranceDetailRepository;
        private PermitDetailRepository permitDetailRepository;
        private TaxDetailRepository taxDetailRepository;
        private FitnessDetailRepository fitnessDetailRepository;
        private BookMasterRepository bookMasterRepository;
        private BookDetailRepository bookDetailRepository;
        private BookIssueSubmitRepository bookIssueSubmitRepository;
        private vStudentExamResultRepository vstudentExamResultRepository;
        private BranchRepository branchRepository;
        private vStudentCreateLoginRepository vstudentCreateLoginRepository;
        private vParentCreateLoginRepository vparentCreateLoginRepository;
        private vTeacherCreateLoginRepository vteacherCreateLoginRepository;
        private vExamMarkEntryRepository vexamMarkEntryRepository;
        private SubjectAllotmentRepository subjectAllotmentRepository;
        private vSubjectAllotmentDataExportRepository vsubjectAllotmentDataExportRepository;
        private vClassTimeTableRepository vclassTimeTableRepository;
        private TeacherDocumentRepository teacherDocumentRepository;
        private StudentLoginRepository studentLoginRepository;
        private ParentLoginRepository parentLoginRepository;
        private AcademicyearRepository academicyearRepository;
        private TeacherLoginRepository teacherLoginRepository;
        private PersonalContectListRepository personalcontectListRepository;
        private TopperNoticeBoardRepository topperNoticeBoardRepository;
        private ImportedURLRepository importedURLRepository;
        private MessageBroadcasteRepository messageBroadcasteRepository;
        private AchievementRepository achievementRepository;
        private ImageFlyerRepository imageFlyerRepository;
        private PhotoGallerdetailRepository photogallerdetailRepository;
        private ThoughtMasterRepository thoughtMasterRepository;
        private FlyerMasterRepository flyermasterRepository;
        private LetterHeadMasterRepository letterheadmasterRepository;
        private FlyerVoiceMasterRepository flyervoicemasterRepository;
        private EmployeeAttendanceEmportRepository employeeattendanceemportRepository;
        private EmployeeShiftMasterRepository employeeshiftmasterRepository;
        private EmployeeAttendanceDailyRepository employeeAttendanceDailyRepository;
        private SessionRepository sessionRepository;
        private AttendanceOnlineClassRepository attendanceOnlineClassRepository;

        private AppModuleRepository appModuleRepository;
        private AppFeatureRepository appFeatureRepository;
        private RoleModuleRepository roleModuleRepository;
        private UserRoleRepository userRoleRepository;

        public UserRoleRepository userRoleservices
        {
            get
            {
                if (this.userRoleRepository == null)
                {
                    this.userRoleRepository = new UserRoleRepository(context);
                }
                return userRoleRepository;
            }
        }


        public RoleModuleRepository roleModuleservices
        {
            get
            {
                if (this.roleModuleRepository == null)
                {
                    this.roleModuleRepository = new RoleModuleRepository(context);
                }
                return roleModuleRepository;
            }
        }

        public AppFeatureRepository appFeatureservices
        {
            get
            {
                if (this.appFeatureRepository == null)
                {
                    this.appFeatureRepository = new AppFeatureRepository(context);
                }
                return appFeatureRepository;
            }
        }


        public AppModuleRepository appModuleservices
        {
            get
            {
                if (this.appModuleRepository == null)
                {
                    this.appModuleRepository = new AppModuleRepository(context);
                }
                return appModuleRepository;
            }
        }

        public AttendanceOnlineClassRepository attendanceOnlineClassservices
        {
            get
            {
                if (this.attendanceOnlineClassRepository == null)
                {
                    this.attendanceOnlineClassRepository = new AttendanceOnlineClassRepository(context);
                }
                return attendanceOnlineClassRepository;
            }
        }


        public EmployeeAttendanceDailyRepository employeeAttendanceDailyservices
        {
            get
            {
                if (this.employeeAttendanceDailyRepository == null)
                {
                    this.employeeAttendanceDailyRepository = new EmployeeAttendanceDailyRepository(context);
                }
                return employeeAttendanceDailyRepository;
            }
        }

        public SessionRepository sessionservices
        {
            get
            {
                if (this.sessionRepository == null)
                {
                    this.sessionRepository = new SessionRepository(context);
                }
                return sessionRepository;
            }
        }


        public EmployeeShiftMasterRepository employeeshiftmasterService
        {
            get
            {
                if (this.employeeshiftmasterRepository == null)
                {
                    this.employeeshiftmasterRepository = new EmployeeShiftMasterRepository(context);
                }
                return employeeshiftmasterRepository;
            }
        }


        public EmployeeAttendanceEmportRepository employeeattendanceemportService
        {
            get
            {
                if (this.employeeattendanceemportRepository == null)
                {
                    this.employeeattendanceemportRepository = new EmployeeAttendanceEmportRepository(context);
                }
                return employeeattendanceemportRepository;
            }
        }


        public FlyerVoiceMasterRepository flyervoicemasterService
        {
            get
            {
                if (this.flyervoicemasterRepository == null)
                {
                    this.flyervoicemasterRepository = new FlyerVoiceMasterRepository(context);
                }
                return flyervoicemasterRepository;
            }
        }



        public LetterHeadMasterRepository letterheadmasterService
        {
            get
            {
                if (this.letterheadmasterRepository == null)
                {
                    this.letterheadmasterRepository = new LetterHeadMasterRepository(context);
                }
                return letterheadmasterRepository;
            }
        }



        public FlyerMasterRepository flyermasterService
        {
            get
            {
                if (this.flyermasterRepository == null)
                {
                    this.flyermasterRepository = new FlyerMasterRepository(context);
                }
                return flyermasterRepository;
            }
        }



        public ThoughtMasterRepository thoughtMasterService
        {
            get
            {
                if (this.thoughtMasterRepository == null)
                {
                    this.thoughtMasterRepository = new ThoughtMasterRepository(context);
                }
                return thoughtMasterRepository;
            }
        }


        public PhotoGallerdetailRepository photogallerdetailService
        {
            get
            {
                if (this.photogallerdetailRepository == null)
                {
                    this.photogallerdetailRepository = new PhotoGallerdetailRepository(context);
                }
                return photogallerdetailRepository;
            }
        }

        public ImageFlyerRepository imageFlyerService
        {
            get
            {
                if (this.imageFlyerRepository == null)
                {
                    this.imageFlyerRepository = new ImageFlyerRepository(context);
                }
                return imageFlyerRepository;
            }
        }


        public AchievementRepository achievementService
        {
            get
            {
                if (this.achievementRepository == null)
                {
                    this.achievementRepository = new AchievementRepository(context);
                }
                return achievementRepository;
            }
        }

        public MessageBroadcasteRepository messageBroadcasteService
        {
            get
            {
                if (this.messageBroadcasteRepository == null)
                {
                    this.messageBroadcasteRepository = new MessageBroadcasteRepository(context);
                }
                return messageBroadcasteRepository;
            }
        }

        public PersonalContectListRepository personalcontectListService
        {
            get
            {
                if (this.personalcontectListRepository == null)
                {
                    this.personalcontectListRepository = new PersonalContectListRepository(context);
                }
                return personalcontectListRepository;
            }
        }
        

        public ImportedURLRepository ImportedURLService
        {
            get
            {
                if (this.importedURLRepository == null)
                {
                    this.importedURLRepository = new ImportedURLRepository(context);
                }
                return importedURLRepository;
            }
        }

        public TopperNoticeBoardRepository TopperNoticeBoardService
        {
            get
            {
                if (this.topperNoticeBoardRepository == null)
                {
                    this.topperNoticeBoardRepository = new TopperNoticeBoardRepository(context);
                }
                return topperNoticeBoardRepository;
            }
        }

        public AcademicyearRepository academicYearMasterService
        {
            get
            {

                if (this.academicyearRepository == null)
                {
                    this.academicyearRepository = new AcademicyearRepository(context);
                }
                return academicyearRepository;
            }
        }

        public TeacherLoginRepository TeacherLoginService
        {
            get
            {
                if (this.teacherLoginRepository == null)
                {
                    this.teacherLoginRepository = new TeacherLoginRepository(context);
                }
                return teacherLoginRepository;
            }
        }

        public StudentLoginRepository StudentLoginService
        {
            get
            {
                if (this.studentLoginRepository == null)
                {
                    this.studentLoginRepository = new StudentLoginRepository(context);
                }
                return studentLoginRepository;
            }
        }

        public ParentLoginRepository ParentLoginService
        {
            get
            {
                if (this.parentLoginRepository == null)
                {
                    this.parentLoginRepository = new ParentLoginRepository(context);
                }
                return parentLoginRepository;
            }
        }

        public vExamMarkEntryRepository vExamMarkEntryService
        {
            get
            {
                if (this.vexamMarkEntryRepository == null)
                {
                    this.vexamMarkEntryRepository = new vExamMarkEntryRepository(context);
                }
                return vexamMarkEntryRepository;
            }
        }

        public TeacherDocumentRepository teacherDocumentService
        {
            get
            {
                if (this.teacherDocumentRepository == null)
                {
                    this.teacherDocumentRepository = new TeacherDocumentRepository(context);
                }
                return teacherDocumentRepository;
            }
        }

        public vClassTimeTableRepository vClassTimeTableService
        {
            get
            {
                if (this.vclassTimeTableRepository == null)
                {
                    this.vclassTimeTableRepository = new vClassTimeTableRepository(context);
                }

                return vclassTimeTableRepository;

            }
        }
        public vSubjectAllotmentDataExportRepository vsubjectAllotmentDataExportService
        {
            get
            {

                if (this.vsubjectAllotmentDataExportRepository == null)
                {
                    this.vsubjectAllotmentDataExportRepository = new vSubjectAllotmentDataExportRepository(context);
                }
                return vsubjectAllotmentDataExportRepository;
            }
        }

        public vTeacherCreateLoginRepository vteacherCreateLoginService
        {
            get
            {
                if (this.vteacherCreateLoginRepository == null)
                {
                    this.vteacherCreateLoginRepository = new vTeacherCreateLoginRepository(context);
                }
                return vteacherCreateLoginRepository;
            }
        }

        public vParentCreateLoginRepository vparentCreateLoginService
        {
            get
            {
                if (this.vparentCreateLoginRepository == null)
                {
                    this.vparentCreateLoginRepository = new vParentCreateLoginRepository(context);
                }
                return vparentCreateLoginRepository;
            }
        } 


        public vStudentCreateLoginRepository vstudentCreateLoginService
        {
            get
            {
                if (this.vstudentCreateLoginRepository == null)
                {
                    this.vstudentCreateLoginRepository = new vStudentCreateLoginRepository(context);
                }
                return vstudentCreateLoginRepository;
            }
        } 

        public BranchRepository branchService
        {
            get
            {
                if (this.branchRepository == null)
                {
                    this.branchRepository = new BranchRepository(context);
                }
                return branchRepository;
            }
        } 


        public vStudentExamResultRepository vStudentExamResultService
        {
            get
            {
                if (this.vstudentExamResultRepository == null)
                {
                    this.vstudentExamResultRepository = new vStudentExamResultRepository(context);
                }
                return vstudentExamResultRepository;
            }
        }

        public SubjectAllotmentRepository SubjectAllotmentService
        {
            get
            {
                if (this.subjectAllotmentRepository == null)
                {
                    this.subjectAllotmentRepository = new SubjectAllotmentRepository(context);
                }

                return subjectAllotmentRepository;

            }
        }


        public BookIssueSubmitRepository bookIssueSubmitService
        {
            get
            {
                if (this.bookIssueSubmitRepository == null)
                {
                    this.bookIssueSubmitRepository = new BookIssueSubmitRepository(context);
                }
                return bookIssueSubmitRepository;
            }
        } 

        public BookDetailRepository bookDetailService
        {
            get
            {
                if (this.bookDetailRepository == null)
                {
                    this.bookDetailRepository = new BookDetailRepository(context);
                }
                return bookDetailRepository;
            }
        } 


        public BookMasterRepository bookMasterService
        {
            get
            {
                if (this.bookMasterRepository == null)
                {
                    this.bookMasterRepository = new BookMasterRepository(context);
                }
                return bookMasterRepository;
            }
        } 

        public FitnessDetailRepository fitnessDetailService
        {
            get
            {
                if (this.fitnessDetailRepository == null)
                {
                    this.fitnessDetailRepository = new FitnessDetailRepository(context);
                }
                return fitnessDetailRepository;
            }
        }
        public TaxDetailRepository taxDetailService
        {
            get
            {
                if (this.taxDetailRepository == null)
                {
                    this.taxDetailRepository = new TaxDetailRepository(context);
                }
                return taxDetailRepository;
            }
        }
        public PermitDetailRepository permitDetailService
        {
            get
            {
                if (this.permitDetailRepository == null)
                {
                    this.permitDetailRepository = new PermitDetailRepository(context);
                }
                return permitDetailRepository;
            }
        }
        public InsuranceDetailRepository insuranceDetailService
        {
            get
            {
                if (this.insuranceDetailRepository == null)
                {
                    this.insuranceDetailRepository = new InsuranceDetailRepository(context);
                }
                return insuranceDetailRepository;
            }
        }
        public AdmissionFormSaleRepository admissionFormSaleService
        {
            get
            {
                if (this.admissionFormSaleRepository == null)
                {
                    this.admissionFormSaleRepository = new AdmissionFormSaleRepository(context);
                }
                return admissionFormSaleRepository;
            }
        }



        public DriverMasterRepository driverMasterService
        {
            get
            {
                if (this.driverMasterRepository == null)
                {
                    this.driverMasterRepository = new DriverMasterRepository(context);
                }
                return driverMasterRepository;
            }
        }

        public BusRoutePlanRepository busRoutePlanService
        {
            get
            {
                if (this.busRoutePlanRepository == null)
                {
                    this.busRoutePlanRepository = new BusRoutePlanRepository(context);
                }
                return busRoutePlanRepository;
            }
        }
        public RouteDetailRepository routeDetailService
        {
            get
            {
                if (this.routeDetailRepository == null)
                {
                    this.routeDetailRepository = new RouteDetailRepository(context);
                }
                return routeDetailRepository;
            }
        }
        public ExamSetupDetailRepository examSetupDetailService
        {
            get
            {
                if (this.examSetupDetailRepository == null)
                {
                    this.examSetupDetailRepository = new ExamSetupDetailRepository(context);
                }
                return examSetupDetailRepository;
            }
        }
        public ExamSetupMasterRepository examSetupMasterService
        {
            get
            {

                if (this.examSetupMasterRepository == null)
                {
                    this.examSetupMasterRepository = new ExamSetupMasterRepository(context);
                }
                return examSetupMasterRepository;
            }
        }
        public ExamSyllabusMasterRepository examSyllabusMasterService
        {
            get
            {

                if (this.examSyllabusMasterRepository == null)
                {
                    this.examSyllabusMasterRepository = new ExamSyllabusMasterRepository(context);
                }
                return examSyllabusMasterRepository;
            }
        }

        public NewsEventMasterRepository NewsEventMasterService
        {
            get
            {
                if (this.newsEventMasterRepository == null)
                {
                    this.newsEventMasterRepository = new NewsEventMasterRepository(context);
                }
                return newsEventMasterRepository;
            }
        }


        public PhotoGalleryMasterRepository PhotoGalleryMasterService
        {
            get
            {
                if (this.photoGalleryMasterRepository == null)
                {
                    this.photoGalleryMasterRepository = new PhotoGalleryMasterRepository(context);
                }
                return photoGalleryMasterRepository;
            }
        }

        public ExamSyllabusDetailRepository examSyllabusDetailService
        {
            get
            {

                if (this.examSyllabusDetailRepository == null)
                {
                    this.examSyllabusDetailRepository = new ExamSyllabusDetailRepository(context);
                }
                return examSyllabusDetailRepository;
            }
        }
        public ExamRemarkMasterRepository examRemarkMasterService
        {
            get
            {

                if (this.examRemarkMasterRepository == null)
                {
                    this.examRemarkMasterRepository = new ExamRemarkMasterRepository(context);
                }
                return examRemarkMasterRepository;
            }
        }
        public FacultyAllotmentDetailRepository FacultyAllotmentDetailService
        {
            get
            {
                if (this.facultyAllotmentDetailRepository == null)
                {
                    this.facultyAllotmentDetailRepository = new FacultyAllotmentDetailRepository(context);
                }
                return facultyAllotmentDetailRepository;
            }
        }
        public FacultyAllotmentMasterRepository FacultyAllotmentMasterService
        {
            get
            {
                if (this.facultyAllotmentMasterRepository == null)
                {
                    this.facultyAllotmentMasterRepository = new FacultyAllotmentMasterRepository(context);
                }
                return facultyAllotmentMasterRepository;
            }
        }
        public TimeScheduleMasterRepository timeScheduleMasterService
        {
            get
            {
                if (this.timeScheduleMasterRepository == null)
                {
                    this.timeScheduleMasterRepository = new TimeScheduleMasterRepository(context);
                }
                return timeScheduleMasterRepository;
            }
        }
       public SubjectLevel2Repository subjectLevel2Service
        {
            get {
                if (this.subjectLevel2repository == null)
                {
                    this.subjectLevel2repository = new SubjectLevel2Repository(context);
                }
                return subjectLevel2repository;
            }
        
        }
       public SubjectLevel3Repository subjectLevel3Service
       {
           get
           {
               if (this.subjectLevel3repository == null)
               {
                   this.subjectLevel3repository = new SubjectLevel3Repository(context);
               }
               return subjectLevel3repository;
           }

       }
        public ClassSyllabusDetailRepository ClassSyllabusDetailService
        {
            get
            {
                if (this.classSyllabusDetailRepository == null)
                {
                    this.classSyllabusDetailRepository = new ClassSyllabusDetailRepository(context);
                }
                return classSyllabusDetailRepository;
            }
        }

        public ClassSyllabusMasterRepository ClassSyllabusMasterService
        {
            get
            {
                if (this.classSyllabusMasterRepository == null)
                {
                    this.classSyllabusMasterRepository = new ClassSyllabusMasterRepository(context);
                }
                return classSyllabusMasterRepository;
            }
        }
        public RackMasterRepository rackMasterService
        {
            get
            {

                if (this.rackMasterRepository == null)
                {
                    this.rackMasterRepository = new RackMasterRepository(context);
                }
                return rackMasterRepository;
            }
        }
        public ShelfMasterRepository shelfMasterService
        {
            get
            {

                if (this.shelfMasterRepository == null)
                {
                    this.shelfMasterRepository = new ShelfMasterRepository(context);
                }
                return shelfMasterRepository;
            }
        }
        public vStudentListFromStudFeeStructureRepository vstudentListFromStudFeesStructureService
        {
            get
            {
                if (this.vstudentListFromStudFeesStructurerepository == null) 
                {
                    this.vstudentListFromStudFeesStructurerepository = new vStudentListFromStudFeeStructureRepository(context);

                }
                return vstudentListFromStudFeesStructurerepository;
            }
        
        }
        public StudentPriviousDetailsRepository studentPriviousDetailsService
        {
            get
            {
                if (this.studentPriviousdetailrepository == null)
                {
                    this.studentPriviousdetailrepository = new StudentPriviousDetailsRepository(context);
                }
                return studentPriviousdetailrepository;
            }
        }
        public TeacherQualificationRepository teacherQualificationService
        {
            get
            {
                if (this.teacherQualificationrepository == null)
                {
                    this.teacherQualificationrepository = new TeacherQualificationRepository(context);
                }
                return teacherQualificationrepository;
            }
        }
        public TeacherExpertiseRepository teacherExpertiseService
        {
            get
            {
                if (this.teacherExpertiserepository == null)
                {
                    this.teacherExpertiserepository = new TeacherExpertiseRepository(context);
                }
                return teacherExpertiserepository;
            }
        }
        public SubjectLevel1Repository subjectLevelService
        {
            get 
            {
                if (this.subjectLevelrepository == null)
                {
                    this.subjectLevelrepository = new SubjectLevel1Repository(context);
                
                }
                return subjectLevelrepository;
            }
          }
        public SubjectExpertiseRepository subjectExpertiseService
        {

            get 
            {
                if (this.subjectExpertieserepository == null)
                {
                    this.subjectExpertieserepository = new SubjectExpertiseRepository(context);
                }
                return subjectExpertieserepository;
            }
        }
        public TeacherAchivmentRepository teacherAchivmentService
        {
            get
            {
                if (this.teacherAchivmentrepository == null)
                {
                    this.teacherAchivmentrepository = new TeacherAchivmentRepository(context);
                
                }
                return teacherAchivmentrepository;
            }
        
        }
        public StudentDocumentRepository studentDocumentService
        {
            get
            {
                if (this.studentDocumentRepository == null)
                {
                    this.studentDocumentRepository = new StudentDocumentRepository(context);
                }
                return studentDocumentRepository;
            }
        }
        public ContectListRepository contectListService
        {
            get
            {
                if (this.contectListRepository == null)
                {
                    this.contectListRepository = new ContectListRepository(context);
                }
                return contectListRepository;
            }
        }
        public SettingMasterRepository SettingMasterService
        {
            get
            {
                if (this.settingMasterRepository == null)
                {
                    this.settingMasterRepository = new SettingMasterRepository(context);
                }
                return settingMasterRepository;
            }
        }
        public vSMSLogDataExportRepository VsmsLogDataExportService
        {
            get
            {
                if (this.vsmsLogDataExportRepository == null)
                {
                    this.vsmsLogDataExportRepository = new vSMSLogDataExportRepository(context);
                }
                return vsmsLogDataExportRepository;
            }
        }

        public VStudentFeesReminderRepository VstudentFeesReminderService
        {
            get
            {
                if (this.VstudentFeesReminderRepository == null)
                {
                    this.VstudentFeesReminderRepository = new VStudentFeesReminderRepository(context);
                }
                return VstudentFeesReminderRepository;
            }
        }
        public UserPermissionRepository userPermissionService
        {
            get
            {
                if (this.userPermissionRepository == null)
                {
                    this.userPermissionRepository = new UserPermissionRepository(context);
                }
                return userPermissionRepository;
            }
        }

        public vFeeSummaryQuaterwiseChartRepository vfeeSummaryQuaterwiseChartService
        {
            get
            {
                if (this.vfeeSummaryQuaterwiseChartRepository == null)
                {
                    this.vfeeSummaryQuaterwiseChartRepository = new vFeeSummaryQuaterwiseChartRepository(context);
                }
                return vfeeSummaryQuaterwiseChartRepository;
            }
        }
        public StudentParentLoginRepository studentParentLoginService
        {
            get
            {
                if (this.studentParentLoginRepository == null)
                {
                    this.studentParentLoginRepository = new StudentParentLoginRepository(context);
                }
                return studentParentLoginRepository;
            }
        }
        public vSessionAdmissionChartRepository vsessionAdmissionChartService
        {
            get
            {
                if (this.vsessionAdmissionChartRepository == null)
                {
                    this.vsessionAdmissionChartRepository = new vSessionAdmissionChartRepository(context);
                }
                return vsessionAdmissionChartRepository;
            }
        }
        public vStudentBirthdayRepository vstudentBirthdayService
        {
            get
            {
                if (this.vstudentBirthdayRepository == null)
                {
                    this.vstudentBirthdayRepository = new vStudentBirthdayRepository(context);
                }
                return vstudentBirthdayRepository;
            }
        }
        public vStudentStrengthChartRepository vstudentStrengthChartService
        {
            get
            {
                if (this.vstudentStrengthChartRepository == null)
                {
                    this.vstudentStrengthChartRepository = new vStudentStrengthChartRepository(context);
                }
                return vstudentStrengthChartRepository;
            }
        }

        public vAttendanceDailyChartRepository vattendanceDailyChartService
        {
            get
            {
                if (this.vattendanceDailyChartRepository == null)
                {
                    this.vattendanceDailyChartRepository = new vAttendanceDailyChartRepository(context);
                }
                return vattendanceDailyChartRepository;
            }
        }
        public vStudentFeesStructDataExportRepository vstudentFeesStructDataExportService
        {
            get
            {
                if (this.vstudentFeesStructDataExportRepository == null)
                {
                    this.vstudentFeesStructDataExportRepository = new vStudentFeesStructDataExportRepository(context);
                }
                return vstudentFeesStructDataExportRepository;
            }
        }

        public vFeesStructureDataExportRepository vFeesStructureDataExportService
        {
            get
            {

                if (this.vfeesStructureDataExportRepository == null)
                {
                    this.vfeesStructureDataExportRepository = new vFeesStructureDataExportRepository(context);
                }
                return vfeesStructureDataExportRepository;
            }
        }
       
        public ApplicationImageRepository applicationImageService
        {
            get
            {

                if (this.applicationImageRepository == null)
                {
                    this.applicationImageRepository = new ApplicationImageRepository(context);
                }
                return applicationImageRepository;
            }
        }

        public vAttendanceDataExportRepository vAttendanceDataExportService
        {
            get
            {
                if (this.vattendanceDataExportRepository == null)
                {
                    this.vattendanceDataExportRepository = new vAttendanceDataExportRepository(context);
                }
                return vattendanceDataExportRepository;
            }
        }



        public vTeacherDataExportRepository vTeacherDataExportService
        {
            get
            {
                if (this.vteacherDataExportRepository == null)
                {
                    this.vteacherDataExportRepository = new vTeacherDataExportRepository(context);
                }
                return vteacherDataExportRepository;
            }
        }

        public vTeacherDataExportDateWiseRepository vTeacherDataExportDateWiseService
        {
            get
            {
                if (this.vTeacherDataExportDateWiseRepository == null)
                {
                    this.vTeacherDataExportDateWiseRepository = new vTeacherDataExportDateWiseRepository(context);
                }
                return vTeacherDataExportDateWiseRepository;
            }
        }

        public vStudentDataExportRepository vStudentDataExportService
        {
            get
            {
                if (this.VStudentDataExportRepository == null)
                {
                    this.VStudentDataExportRepository = new vStudentDataExportRepository(context);
                }
                return VStudentDataExportRepository;
            }
        }

        public SetupRepository setupService
        {
            get
            {
                if (this.setupRepository == null)
                {
                    this.setupRepository = new SetupRepository(context);
                }
                return setupRepository;
            }
        }
        public FeesReceiptRepository feesReceiptService
        {
            get
            {
                if (this.feesReceiptRepository == null)
                {
                    this.feesReceiptRepository = new FeesReceiptRepository(context);
                }
                return feesReceiptRepository;
            }
        }
        public FeesCollectionMasterRepository feeCollectionMasterService
        {
            get
            {
                if (this.feesCollectionMasterRepository == null)
                {
                    this.feesCollectionMasterRepository = new FeesCollectionMasterRepository(context);
                }
                return feesCollectionMasterRepository;
            }
        }

        public FeesCollectionDetailRepository feeCollectionDetailService
        {
            get
            {
                if (this.feesCollectionDetailRepository == null)
                {
                    this.feesCollectionDetailRepository = new FeesCollectionDetailRepository(context);
                }
                return feesCollectionDetailRepository;
            }
        }

        public StudentFeesMasterRepository StudentFeesMasterService
        {
            get
            {
                if (this.studentFeesMasterRepository == null)
                {
                    this.studentFeesMasterRepository = new StudentFeesMasterRepository(context);
                }
                return studentFeesMasterRepository;
            }
        }

        public StudentFeesDetailRepository StudentFeesDetailService
        {
            get
            {
                if (this.studentFeesDetailRepository == null)
                {
                    this.studentFeesDetailRepository = new StudentFeesDetailRepository(context);
                }
                return studentFeesDetailRepository;
            }
        }

        public SchoolMasterRepository schoolMasterService
        {
            get
            {
                if (this.schoolMasterRepository == null)
                {
                    this.schoolMasterRepository = new SchoolMasterRepository(context);
                }
                return schoolMasterRepository;
            }
        }
        public UserLoginRepository userLoginService
        {
            get
            {
                if (this.userLoginRepository == null)
                {
                    this.userLoginRepository = new UserLoginRepository(context);
                }
                return userLoginRepository;
            }
        }
        public AttendanceStudentRepository attendanceStudentService
        {
            get
            {
                if (this.attendanceStudentRepository == null)
                {
                    this.attendanceStudentRepository = new AttendanceStudentRepository(context);
                }
                return attendanceStudentRepository;
            }
        }
        public ClassRepository ClassService
        {
            get
            {
                if (this.classRepository == null)
                {
                    this.classRepository = new ClassRepository(context);
                }
                return classRepository;
            }
        }
        public SectionRepository SectionRepositoryViewModel
        {
            get
            {

                if (this.sectionRepository == null)
                {
                    this.sectionRepository = new SectionRepository(context);
                }
                return sectionRepository;
            }
        }
        public SMSTypeRepository SMSTypeService
        {
            get
            {

                if (this.smsTypeRepository == null)
                {
                    this.smsTypeRepository = new SMSTypeRepository(context);
                }
                return smsTypeRepository;
            }
        }
        public ExamCategoryRepository examCategoryService
        {
            get
            {

                if (this.examCategoryRepository == null)
                {
                    this.examCategoryRepository = new ExamCategoryRepository(context);
                }
                return examCategoryRepository;
            }
        }

        public HouseRepository houseRepositoryViewModel
        {
            get
            {

                if (this.houseRepository == null)
                {
                    this.houseRepository = new HouseRepository(context);
                }
                return houseRepository;
            }
        }
        public ClassCategoryRepository classCategoryRepositoryViewModel
        {
            get
            {

                if (this.classCategoryRepository == null)
                {
                    this.classCategoryRepository = new ClassCategoryRepository(context);
                }
                return classCategoryRepository;
            }
        }
        public SMSTemplateRepository smsTemplateService
        {
            get
            {

                if (this.smsTemplatesRepository == null)
                {
                    this.smsTemplatesRepository = new SMSTemplateRepository(context);
                }
                return smsTemplatesRepository;
            }
        }
       
        public FeesCategoryRepository feesCategoryService
        {
            get
            {

                if (this.feesCategoryRepository == null)
                {
                    this.feesCategoryRepository = new FeesCategoryRepository(context);
                }
                return feesCategoryRepository;
            }
        }
        public StudentRegistrationRepository studentRegistrationService
        {
            get
            {

                if (this.studentRegistrationRepository == null)
                {
                    this.studentRegistrationRepository = new StudentRegistrationRepository(context);
                }
                return studentRegistrationRepository;
            }
        }

        public ClassSetupRepository classSetupService
        {
            get
            {

                if (this.classSetupRepository == null)
                {
                    this.classSetupRepository = new ClassSetupRepository(context);
                }
                return classSetupRepository;
            }
        }
        public AccountMasterRepository accountMasterService
        {
            get
            {

                if (this.accountMasterRepository == null)
                {
                    this.accountMasterRepository = new AccountMasterRepository(context);
                }
                return accountMasterRepository;
            }
        }
        public VStudentDetailRepository VstudentDetailService
        {
            get
            {
                if (this.VstudentDetailRepository == null)
                {
                    this.VstudentDetailRepository = new VStudentDetailRepository(context);
                }
                return VstudentDetailRepository;
            }
        }
      
        public AcademicyearRepository academicYearService
        {
            get
            {

                if (this.academicYearRepository == null)
                {
                    this.academicYearRepository = new AcademicyearRepository(context);
                }
                return academicYearRepository;
            }
        }
        public BusMasterRepository busMasterService
        {
            get
            {

                if (this.busMasterRepository == null)
                {
                    this.busMasterRepository = new BusMasterRepository(context);
                }
                return busMasterRepository;
            }
        }
        public DepartmentMasterRepository departmentMasterService
        {
            get
            {

                if (this.departmentMasterRepository == null)
                {
                    this.departmentMasterRepository = new DepartmentMasterRepository(context);
                }
                return departmentMasterRepository;
            }
        }
        public DesignationMasterRepository designationMasterService
        {
            get
            {

                if (this.designationMasterRepository == null)
                {
                    this.designationMasterRepository = new DesignationMasterRepository(context);
                }
                return designationMasterRepository;
            }
        }
        public BusStopMasterRepository busStopMasterService
        {
            get
            {

                if (this.busStopMasterRepository == null)
                {
                    this.busStopMasterRepository = new BusStopMasterRepository(context);
                }
                return busStopMasterRepository;
            }
        }

        public RouteMasterRepository routeMasterService
        {
            get
            {

                if (this.routeMasterRepository == null)
                {
                    this.routeMasterRepository = new RouteMasterRepository(context);
                }
                return routeMasterRepository;
            }
        }
      
        public GradeMasterRepository gradeMasterService
        {
            get
            {

                if (this.gradeMasterRepository == null)
                {
                    this.gradeMasterRepository = new GradeMasterRepository(context);
                }
                return gradeMasterRepository;
            }
        }
        public TeacherRegistrationRepository teacherRegistrationService
        {
            get
            {

                if (this.teacherRegistrationRepository == null)
                {
                    this.teacherRegistrationRepository = new TeacherRegistrationRepository(context);
                }
                return teacherRegistrationRepository;
            }
        }
        public InstalmentMasterRepository instalmentMasterService
        {
            get
            {

                if (this.instalmentMasterRepository == null)
                {
                    this.instalmentMasterRepository = new InstalmentMasterRepository(context);
                }
                return instalmentMasterRepository;
            }
        }

        public OwnerMasterRepository ownerMasterService
        {
            get
            {

                if (this.ownerMasterRepository == null)
                {
                    this.ownerMasterRepository = new OwnerMasterRepository(context);
                }
                return ownerMasterRepository;
            }
        }

        public StudentSessionRepository studentSessionService
        {
            get
            {
                if (this.studentSessionRepository == null)
                {
                    this.studentSessionRepository = new StudentSessionRepository(context);
                }
                return studentSessionRepository;
            }
        }

        public AttendanceClassRepository attendanceClassService
        {
            get
            {
                if (this.attendanceClassRepository == null)
                {
                    this.attendanceClassRepository = new AttendanceClassRepository(context);
                }
                return attendanceClassRepository;
            }
        }
        public SendMessegeRepository sendMessegeService
        {
            get
            {
                if (this.sendMessegeRepository == null)
                {
                    this.sendMessegeRepository = new SendMessegeRepository(context);
                }
                return sendMessegeRepository;
            }
        }


        public FeesHeadRepository feesHeadService
        {
            get
            {
                if (this.feesHeadRepository == null)
                {
                    this.feesHeadRepository = new FeesHeadRepository(context);
                }
                return feesHeadRepository;
            }
        }

        public FeeTermRepository feeTermService
        {
            get
            {
                if (this.feeTermRepository == null)
                {
                    this.feeTermRepository = new FeeTermRepository(context);
                }
                return feeTermRepository;
            }
        }

        public FeesStructureMasterRepository feesStructureMasterService
        {
            get
            {
                if (this.feesStructureMasterRepository == null)
                {
                    this.feesStructureMasterRepository = new FeesStructureMasterRepository(context);
                }
                return feesStructureMasterRepository;
            }
        }
        public FeesStructureDetailRepository feesStructureDetailService
        {
            get
            {
                if (this.feesStructureDetailRepository == null)
                {
                    this.feesStructureDetailRepository = new FeesStructureDetailRepository(context);
                }
                return feesStructureDetailRepository;
            }
        }
        public vListFeesStructureRepository vlistFeesStructureService
        {
            get
            {
                if (this.vlistFeesStructureRepository == null)
                {
                    this.vlistFeesStructureRepository = new vListFeesStructureRepository(context);
                }
                return vlistFeesStructureRepository;
            }
        }

        public NoticeBoardRepository noticeBoardService
        {
            get
            {
                if (this.noticeBoardRepository == null)
                {
                    this.noticeBoardRepository = new NoticeBoardRepository(context);
                }
                return noticeBoardRepository;
            }
        }









        public void Save()
        {
            context.SaveChanges();

         
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}