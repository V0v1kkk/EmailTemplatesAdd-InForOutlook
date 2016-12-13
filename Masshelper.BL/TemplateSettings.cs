namespace Masshelper.BL
{
    public class MailsTemplate
    {
        public int Templateid;
        public string Templatename;
        public string Templadescription;
        public byte[] TemplateBody;

        public MailsTemplate()
        {
            Templateid = 0;
            Templadescription = "";
            TemplateBody = null;
        }

    }
}
