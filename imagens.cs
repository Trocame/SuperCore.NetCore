using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SuperCore
{
    public class imagens
    {
        /// <summary>
        /// redimensionar imagem
        /// </summary>
        /// <param name="img">Imagem a ser redimessionada</param>
        /// <param name="maxWidth">Largura maxima que a imagem pode assumir</param>
        /// <param name="maxHeight">altura maxima que a imagem pode assumir</param>
        /// <returns>Imagem redimenssionada</returns>
        public static Image resizeImage(Image img, int maxWidth, int maxHeight)
        {

            // imagem deitada
            if (img.Width > img.Height)
                return resizeWidth(img, maxWidth);
            //imagem em pé
            else
                return resizeHeight(img, maxHeight);

        }


        /// <summary>
        /// redimensionar imagem com uma largura estipulada e calcula a altura de forma a manter a proporção corrta da imagem
        /// </summary>
        /// <param name="img">Imagem a ser redimessionada</param>
        /// <param name="newWidth">Largura maxima que a imagem pode assumir</param>        
        /// <returns>Imagem redimenssionada</returns>
        public static Image resizeWidth(Image image, int newWidth)
        {

            int newHeight = (newWidth * image.Height) / image.Width;

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth,  newHeight);

            return newImage;
           
        }

        /// <summary>
        ///  redimensionar imagem com uma altura estipulada e calcula a largura de forma a manter a proporção corrta da imagem
        /// </summary>
        /// <param name="img">Imagem a ser redimessionada</param>
        /// <param name="newHeight">altura maxima que a imagem pode assumir</param>
        /// <returns>Imagem redimenssionada</returns>
        public static Image resizeHeight(Image image, int newHeight)
        {
            int newWidth = (newHeight * image.Width) / image.Height;

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;            

        }

    }
}
