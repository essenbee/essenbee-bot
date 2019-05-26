namespace Essenbee.Bot.Core.Data
{
    public class ProjectText : DataEntity
    {
        public string Text { get; set; }

        public ProjectText()
        {
        }

        public ProjectText(string text)
        {
            Text = text;
        }
    }
}
