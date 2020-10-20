using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Dependencies
{
    public static class Procedures
    {
        /// -----------------------------SP GET---------------------------------
        public static string GetAllStaff            = "sp_getAllStaff";
        public static string GetAllLogin            = "sp_getAllLogin";
        public static string GetAllTracker          = "sp_getAllTracker";
        public static string GetAllStudent          = "sp_getAllStudent";
        public static string GetAllDesignation      = "sp_getAllDesignation";
        public static string GetAllInstitution      = "sp_getAllInstitution";
        /// -----------------------------SP UPDATE---------------------------------
        public static string UpdateBatch            = "sp_updateBatch";
        public static string UpdateClass            = "sp_updateClass";
        public static string UpdateStaff            = "sp_updateStaff";
        public static string UpdateLogin            = "sp_updateLogin";
        public static string UpdateResult           = "sp_updateResult";
        public static string UpdateSubject          = "sp_updateSubject";
        public static string UpdateStudent          = "sp_updateStudent";
        public static string UpdateExamManager      = "sp_updateExamManager";
        public static string UpdateDesignation      = "sp_updateDesignation";
        public static string UpdateInstitution      = "sp_updateInstitution";
        public static string UpdateClassRoutine     = "sp_updateClassRoutine";
        public static string UpdateGradingSystem    = "sp_updateGradingSystem";
        public static string UpdateFeesCollection   = "sp_UpdateFeesCollection";
        public static string UpdateSubjectManager   = "sp_updateSubjectManager";
        public static string UpdateAcademicGrading  = "sp_UpdateAcademicGrading";
        public static string UpdateStaffAttendance  = "sp_updateStaffAttendance";
        public static string UpdateExamInformation  = "sp_updateExamInformation";
        public static string UpdateStudentAdmission = "sp_updateStudentAdmission";
        public static string UpdateStudentAttendance = "sp_updateStudentAttendance";
        /// -----------------------------SP SAVE---------------------------------
        public static string SaveBatch              = "sp_saveBatch";
        public static string SaveClass              = "sp_saveClass";
        public static string SaveStaff              = "sp_saveStaff";
        public static string SaveResult             = "sp_saveResult";
        public static string SaveSubject            = "sp_saveSubject";
        public static string SaveTracker            = "sp_saveTracker";
        public static string SaveStudent            = "sp_saveStudent";
        public static string SaveExamManager        = "sp_saveExamManager";
        public static string SaveInstitution        = "sp_saveInstitution";
        public static string SaveDesignation        = "sp_saveDesignation";
        public static string SaveClassRoutine       = "sp_saveClassRoutine";
        public static string SaveGradingSystem      = "sp_saveGradingSystem";
        public static string SaveSubjectManager     = "sp_saveSubjectManager";
        public static string SaveFeesCollection     = "sp_SaveFeesCollection";
        public static string SaveAcademicGrading    = "sp_SaveAcademicGrading";
        public static string SaveStaffAttendance    = "sp_saveStaffAttendance";
        public static string SaveExamInformation    = "sp_saveExamInformation";
        public static string SaveStudentAdmission   = "sp_saveStudentAdmission";
        public static string SaveStudentAttendance  = "sp_saveStudentAttendance";
        /// -----------------------------SP DELETE---------------------------------
        public static string DeleteClass            = "sp_deleteClass";
        public static string DeleteBatch            = "sp_deleteBatch";
        public static string DeleteResult           = "sp_deleteResult";
        public static string DeleteSubject          = "sp_deleteSubject";
        public static string DeleteExamManager      = "sp_deleteExamManager";
        public static string DeleteDesignation      = "sp_deleteDesignation";
        public static string DeleteClassRoutine     = "sp_deleteClassRoutine";
        public static string DeleteGradingSystem    = "sp_deleteGradingSystem";
        public static string DeleteSubjectManager   = "sp_deleteSubjectManager";
        public static string DeleteFeesCollection   = "sp_DeleteFeesCollection";
        public static string DeleteAcademicGrading  = "sp_DeleteAcademicGrading";
        public static string DeleteStaffAttendance  = "sp_deleteStaffAttendance";
        public static string DeleteExamInformation  = "sp_deleteExamInformation";
        public static string DeleteStudentAdmission = "sp_deleteStudentAdmission";
        /// Large Version of SP DELETE
        public static string DeleteStudentAttendance                = "sp_deleteStudentAttendance";
        public static string DeleteAcademicGradingByGradingSystem   = "sp_deleteAcademicGradingByGradingSystemId";

    }                                                
}
