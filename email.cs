using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System;
using System.Net;
using System.Text;

namespace SuperCore
{

    /// <summary>
    /// Objeto para envio de email
    /// </summary>
    /// <![CDATA[
    ///Email em = new Email();
    ///em.Assunto = "teste2";
    ///em.Host = "smtp.uniodonto-rio.com";
    ///EMailAddress email = new EMailAddress("guto2902@gmail.com");
    ///List<EMailAddress> lemail = new List<EMailAddress>();
    ///lemail.Add(email);
    ///em.Para = lemail;
    ///EMailAddress rem_email = new EMailAddress("guto2902@hotmail.com");
    ///em.RemetentePadrao = rem_email;
    ///em.IsBodyHtml = true;
    ///em.ResponderPara = string.Empty;
    ///em.Porta = 25;
    ///em.Anexar(@"D:\Uniodonto\web\_recursos2\temas\images\icone_planoN1.png");
    ///em.Anexar(@"D:\Uniodonto\web\_recursos2\temas\images\icone_planoN2.png");
    ///em.addBodyParam("nome", "Luis");
    ///em.addBodyParam("cod", "123456");
    ///em.addBodyParam("senha", "987456321");
    ///em.loadBodyFromFile("D:/Uniodonto/web/admcooperados/trunk/_recursos/emails/Cooperado.html");   
    ///em.Enviar();]]>

    public class Email
    {


        //Parâmetros existentes dentro de um arquivo html usado como corpo de um e-mail.
        List<KeyValuePair<string, string>> fileBodyParametros = new List<KeyValuePair<string, string>>();


        /// <summary>
        /// coleção de string com o caminho fisico dos arquivos para serem enviados como anexo
        /// </summary>
        List<Attachment> mailAttachmentCollection = new List<Attachment>();        

        /// <summary>
        /// endereço de email para a propriedade reply to
        /// </summary>        
        public EMailAddress ResponderPara { get; set; }

        /// <summary>
        /// assunto do email
        /// </summary>
        [Required(ErrorMessage = "Informe o assunto do e-mail")]
        public string Assunto { get; set; }

        /// <summary>
        /// indica se o corpo do mensagem foi formatado em html
        /// </summary>
        [Required(ErrorMessage = "Informe se o corpo do e-mail é em html")]
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// indica se o ssl sera utilizado
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// conteudo do email
        /// </summary>
        [Required(ErrorMessage = "Informe o corpo da mensagem")]
        public string Mensagem { get; set; }

        /// <summary>
        /// listagem de enderços de emails para aos quais serão enviados a mens
        /// </summary>
        [Required(ErrorMessage = "Informe o endereço de e-mail do destinatário")]
        public List<EMailAddress> Para { get; set; }

        /// <summary>
        /// listagem de enderços de emails para aos quais serão enviados em copia
        /// </summary>
        public List<EMailAddress> CopiaPara { get; set; }


        /// <summary>
        /// listagem de enderços de emails para aos quais serão enviados em copia oculta
        /// </summary>
        public List<EMailAddress> CopiaOcultaPara { get; set; }


        /// <summary>
        /// enderço de emails do remetente da mensagem
        /// </summary>
        public EMailAddress RemetentePadrao { get; set; }

        /// <summary>
        /// Porta do serviço de smtp, o padrao é 25
        /// </summary>
        [Required(ErrorMessage = "Informe a porta do host")]
        public int Porta { get; set; }


        /// <summary>
        /// endereço do smtp
        /// </summary>
        [Required(ErrorMessage = "Informe o host do e-mail")]
        public string Host { get; set; }


        /// <summary>
        /// senha do serviço de smtp
        /// </summary>
        [Required(ErrorMessage = "Informe a senha do smtp")]
        public string Senha { get; set; }


        /// <summary>
        /// senha do serviço de smtp
        /// </summary>
        [Required(ErrorMessage = "Informe o usuario do smtp")]
        public string UserName { get; set; }


        public Email()
        {

        }




        /// <summary>
        /// Metodo para envio do e-mail
        /// </summary>
        public void Enviar()
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.From = new System.Net.Mail.MailAddress(RemetentePadrao.ToString());

           
            if ( this.EnableSsl != true && this.EnableSsl != false)
                this.EnableSsl = false;


            //Configura para quem a resposta do e-mail será enviada.
            if ( !string.IsNullOrEmpty(  this.ResponderPara.ToString() ))
            {
                //Configura o e-mail de resposta
                mail.ReplyToList.Add(this.ResponderPara.ToString());
            }

            //Adiciona os endereços de destino
            foreach (EMailAddress email in this.Para)
            {
                mail.To.Add(email.ToString());
            }

            if (this.CopiaPara!=null)
            {
                foreach (EMailAddress email in this.CopiaPara)
                {
                    mail.CC.Add(email.ToString());
                } 
            }

            if (this.CopiaOcultaPara != null)
            {
                foreach (EMailAddress email in this.CopiaOcultaPara)
                {
                    mail.Bcc.Add(email.ToString());
                }
            }
            mail.Subject = this.Assunto;

            System.Text.StringBuilder mensagem = new System.Text.StringBuilder();
            mensagem.Append(this.Mensagem);

            System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Mensagem.RemoverTagHtml(), null, System.Net.Mime.MediaTypeNames.Text.Plain);

            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Mensagem ,null, System.Net.Mime.MediaTypeNames.Text.Html);

           // mail.Body = mensagem.ToString();
            mail.IsBodyHtml = this.IsBodyHtml;            

            mail.AlternateViews.Add(plainView);

            mail.AlternateViews.Add(htmlView);




                foreach(Attachment at in mailAttachmentCollection)
                {
                    

                    mail.Attachments.Add(at);

                }


           

            //Envia o e-mail
            mail.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(this.Host, this.Porta);

            if (this.Senha != null )
            {

                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential( (this.UserName!=null ? this.UserName :  this.RemetentePadrao.ToString()), this.Senha);

                smtpClient.UseDefaultCredentials = false;

                smtpClient.Credentials = SMTPUserInfo;

                smtpClient.EnableSsl = this.EnableSsl;
            }
            

            smtpClient.Send(mail);
        }

        public void loadFromHttp(string url)
        {

            string conteudo = "";

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                conteudo = readStream.ReadToEnd();

                foreach (var item in fileBodyParametros)
                {
                    if (conteudo.Contains(item.Key))
                    {
                        //Se na linha existir o parâmetro especificado, o valor dessa linha é substituído pelo valor do parâmetro.
                        conteudo = conteudo.Replace(item.Key, item.Value);
                    }
                }

                response.Close();
                readStream.Close();
            }

            this.Mensagem = conteudo;
        }


        /// <summary>
        /// Carrega o corpo Html que conterá o e-mail.
        /// </summary>
        /// <param name="filePath">Caminho Fisico do Arquivo do HTML.</param>
        public void loadBodyFromFile(string filePath)
        {
            //Abre o arquivo para leitura
            //string ARQUIVO = System.Web.HttpServerUtility.Server.MapPath(filePath);

            //Classe de manipulação de fluxo que lerá o arquivo
            System.IO.StreamReader fluxoLeitura;
            fluxoLeitura = System.IO.File.OpenText(filePath);

            //Obtém todo o conteúdo do arquivo
            //string conteudo = fluxoLeitura.ReadToEnd();
            string conteudo = "";

            //Se houver parâmetros, há necessidade de substituí-los no corpo HTML.
            if (fileBodyParametros.Count > 0)
            {
                //Enquanto não chegar ao fim do arquivo o fluxo é lido linha a linha.
                while (fluxoLeitura.Peek() != -1)
                {
                    //Lê linha a linha
                    string linha = fluxoLeitura.ReadLine();


                    //Verifica se na linha existe algum dos parâmetros.
                    foreach (var item in fileBodyParametros)
                    {
                        if (linha.Contains(item.Key))
                        {
                            //Se na linha existir o parâmetro especificado, o valor dessa linha é substituído pelo valor do parâmetro.
                           linha= linha.Replace(item.Key, item.Value);
                        }
                    }

                    //Vai formando o conteúdo linha por linha.
                    conteudo += linha;
                }
            }

            //Fecha o arquivo.
            fluxoLeitura.Close();



            this.Mensagem = conteudo;
        }

        /// <summary>
        /// Substitui os parâmetros @@xxx configurados no arquivo html que será enviado no email.
        /// </summary>
        /// <param name="nome">Nome do parâmetro que está configurado no arquivo</param>
        /// <param name="valor">Valor pelo qual o parâmetro será substituído</param>
        public void addBodyParam(string nome, string valor)
        {
            nome = "@@" + nome;

            //A propriedade Text de ListItem é usada para representar o nome do parâmetro.
            //Obs: As classes ListItem e ListItemCollection são usadas enquanto não forem implementadas classes para tal operação.
            fileBodyParametros.Add(new KeyValuePair<string, string>(nome, valor));
        }



        public void addMailAttachmentCollection(string attachmentFilename, byte[] byteArray, ContentType type)
        {

            
            mailAttachmentCollection.Add(Anexar(attachmentFilename,byteArray,type));

        }

        public void addMailAttachmentCollection(string attachmentFile)
        {

            mailAttachmentCollection.Add(Anexar(attachmentFile));

        }

        /// <summary>
        /// Anexa arquivo ao email para envio
        /// </summary>
        /// <param name="attachmentFilename">string com caminho fisico para o arquivo que sera anexados</param>
        private Attachment Anexar(string attachmentFilename)
        {                          
           
                Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                disposition.FileName = Path.GetFileName(attachmentFilename);
                disposition.Size = new FileInfo(attachmentFilename).Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;

            // mailAttachmentCollection.Add(attachment);

            return attachment;
        }

        /// <summary>
        /// Anexa arquivo ao email para envio
        /// </summary>
        /// <param name="attachmentFilename">nome do arquivos que sera anexado</param>
        /// <param name="byteArray">array de bytes do arqyuivo</param>
        /// <param name="type">mine type do arquivo</param>
        private Attachment Anexar(string attachmentFilename, byte[] byteArray, ContentType type)
        {
            try
            {
                
                Stream st = new MemoryStream(byteArray);

                Attachment attachment = new Attachment(st, attachmentFilename, type.ToString());
                //ContentDisposition disposition = attachment.ContentDisposition;
                //disposition.CreationDate = DateTime.Now;
                //disposition.ModificationDate = DateTime.Now;
                //disposition.ReadDate = DateTime.Now;
                //disposition.FileName = attachmentFilename;
                //disposition.Size = st.Length;
                //disposition.DispositionType = DispositionTypeNames.Attachment;

                return attachment;
            }
            catch(Exception ex)
            {
                return null;

            }

        }

    }
}




