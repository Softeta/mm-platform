namespace Persistence.Customization.TableStorage
{
    public static class FileCacheTableStorage
    {
        public static class Candidate
        {
            public static class Category
            {
                public const string Video = "CandidateVideo";
                public const string Certificate = "CandidateCertificate";
                public const string Picture = "CandidatePicture";
                public const string CurriculumVitae = "CurriculumVitae";
                public const string MotivationVideo = "CandidateJobMotivationVideo";
            }
            public const string FilePartitionKey = "CandidateFileCache";
        } 

        public static class Company
        {
            public static class Category
            {
                public const string ContactPersonPicture = "CompanyContactPersonPicture";
                public const string Logo = "CompanyLogo";
            }
            public const string FilePartitionKey = "CompanyFileCache";
        }

        public const string TableName = "FileCaches";
    }
}
