namespace Persistence.Customization.FileStorage
{
    public class BlobContainerSettings
    {
        public string CandidatePicturesContainer { get; set; } = null!;
        public string CandidateCertificatesContainer { get; set; } = null!;
        public string CandidateVideosContainer { get; set; } = null!;
        public string CandidateBiosContainer { get; set; } = null!; 
        public string CandidateCurriculumVitaesContainer { get; set; } = null!;
        public string CandidateMotivationVideosContainer { get;set; } = null!;
        public string CandidateTestRaports { get; set; } = null!;
    }
}
