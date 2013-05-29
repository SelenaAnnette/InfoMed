namespace MobileWebApi.Infrastructure
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    using DataLayer.Persistence.AllergicReaction;
    using DataLayer.Persistence.Complaint;
    using DataLayer.Persistence.Consultation;
    using DataLayer.Persistence.Diagnosis;
    using DataLayer.Persistence.Disease;
    using DataLayer.Persistence.Group;
    using DataLayer.Persistence.Hospital;
    using DataLayer.Persistence.LabAnalyze;
    using DataLayer.Persistence.Measuring;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Operation;
    using DataLayer.Persistence.Person;
    using DataLayer.Persistence.Research;
    using DataLayer.Persistence.RiskFactor;
    using DataLayer.Persistence.Symptom;

    using Ninject;

    using ServerLogic.Logger;
    using ServerLogic.Security;

    /// <summary>
    /// The ninject controller factory.
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// The ninject kernel.
        /// </summary>
        private readonly IKernel ninjectKernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectControllerFactory"/> class.
        /// </summary>
        public NinjectControllerFactory()
        {
            this.ninjectKernel = new StandardKernel();
            this.AddBindings();
        }

        /// <summary>
        /// The get controller instance.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <param name="controllerType">
        /// The controller type.
        /// </param>
        /// <returns>
        /// The System.Web.Mvc.IController.
        /// </returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)this.ninjectKernel.Get(controllerType);
        }

        /// <summary>
        /// The add bindings.
        /// </summary>
        private void AddBindings()
        {
            var mainDataBaseConnectionString = Properties.Settings.Default.MainDBConnectionString;
            var trashDataBaseConnectionString = Properties.Settings.Default.TrashDBConnectionString;

            this.ninjectKernel.Bind<IPersonRepository>().To<PersonRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IGroupRepository>().To<GroupRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<ISymptomRepository>().To<SymptomRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IRiskFactorRepository>().To<RiskFactorRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IMedicamentRepository>().To<MedicamentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IMeasuringTypeRepository>().To<MeasuringTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<ICredentialsRepository>().To<CredentialsRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IContactTypeRepository>().To<ContactTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IAssignedSymptomRepository>().To<AssignedSymptomRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IAssignedRiskFactorRepository>().To<AssignedRiskFactorRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IAssignedMedicamentRepository>().To<AssignedMedicamentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonPersonRepository>().To<PersonPersonRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonGroupRepository>().To<PersonGroupRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonContactRepository>().To<PersonContactRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            this.ninjectKernel.Bind<IAllergicReactionRepository>().To<AllergicReactionRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonAllergicReactionRepository>().To<PersonAllergicReactionRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IDiseaseRepository>().To<DiseaseRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonDiseaseRepository>().To<PersonDiseaseRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IConsultationTypeRepository>().To<ConsultationTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationRepository>().To<PersonConsultationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IHospitalDepartmentRepository>().To<HospitalDepartmentRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IHospitalRepository>().To<HospitalRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonHospitalizationRepository>().To<PersonHospitalizationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IOperationRepository>().To<OperationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonOperationRepository>().To<PersonOperationRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            this.ninjectKernel.Bind<IPersonConsultationComplaintRepository>().To<PersonConsultationComplaintRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationSymptomRepository>().To<PersonConsultationSymptomRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationMeasuringRepository>().To<PersonConsultationMeasuringRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<ILabAnalyzeTypeRepository>().To<LabAnalyzeTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationLabAnalyzeRepository>().To<PersonConsultationLabAnalyzeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IResearchRepository>().To<ResearchRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationResearchRepository>().To<PersonConsultationResearchRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            this.ninjectKernel.Bind<IDiagnosisRepository>().To<DiagnosisRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IDiagnosisTypeRepository>().To<DiagnosisTypeRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonConsultationDiagnosisRepository>().To<PersonConsultationDiagnosisRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            this.ninjectKernel.Bind<IAssignedMedicamentMeasuringRepository>().To<AssignedMedicamentMeasuringRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IMedicamentApplicationWayRepository>().To<MedicamentApplicationWayRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IMedicamentFormRepository>().To<MedicamentFormRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);
            this.ninjectKernel.Bind<IAssignedMeasuringRepository>().To<AssignedMeasuringRepository>().WithConstructorArgument("connectionString", mainDataBaseConnectionString);

            this.ninjectKernel.Bind<IMessageRepository>().To<MessageRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<INotificationRepository>().To<NotificationRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonMeasuringRepository>().To<PersonMeasuringRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonMedicamentRepository>().To<PersonMedicamentRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonRiskFactorRepository>().To<PersonRiskFactorRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IPersonSymptomRepository>().To<PersonSymptomRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IMeasuringNotificationRepository>().To<MeasuringNotificationRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);
            this.ninjectKernel.Bind<IOnceRiskFactorNotificationRepository>().To<OnceRiskFactorNotificationRepository>().WithConstructorArgument("connectionString", trashDataBaseConnectionString);

            this.ninjectKernel.Bind<IAuthenticationProvider>().To<AuthenticationProvider>();
            this.ninjectKernel.Bind<ILogger>().To<FileLogger>();
        }
    }
}