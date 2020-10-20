using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Database
{
    public static class Views
    {
        private static readonly string Select = "SELECT * FROM ";

        public static string ALLClass               = String.Concat(Select, "vw_class");
        public static string ALLBatch               = String.Concat(Select, "vw_batch");
        public static string ALLTracer              = String.Concat(Select, "vw_tracer");
        public static string ALLResult              = String.Concat(Select, "vw_Result");
        public static string ALLSubject             = String.Concat(Select, "vw_subject");
        public static string ALLExamManager         = String.Concat(Select, "vw_ExamManager");
        public static string ALLClassRoutine        = String.Concat(Select, "vw_ClassRoutine");
        public static string ALLGradingSystem       = String.Concat(Select, "vw_GradingSystem");
        public static string ALLSubjectManager      = String.Concat(Select, "vw_subjectManager");
        public static string ALLFeesCollection      = String.Concat(Select, "vw_FeesCollection");
        public static string ALLStaffAttendance     = String.Concat(Select, "vw_StaffAttendance");
        public static string ALLExamInformation     = String.Concat(Select, "vw_ExamInformation");
        public static string ALLAcademicGrading     = String.Concat(Select, "vw_AcademicGrading");
        public static string ALLStudentAdmission    = String.Concat(Select, "vw_studentAdmission");
        public static string ALLStudentAttendance   = String.Concat(Select, "vw_StudentAttendance");

    }
}
