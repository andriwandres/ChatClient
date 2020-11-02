namespace Core.Domain.Entities
{
    public class Translation
    {
        public int TranslationId { get; set; }
        public int LanguageId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public Language Language { get; set; }
    }
}
