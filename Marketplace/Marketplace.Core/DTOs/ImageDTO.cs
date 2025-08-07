namespace Marketplace.Core.DTOs
{
    public class ImageDTO
    {
        public string id { get; set; }
        public string FilePath { get; set; }
        public string AltText { get; set; }

        //Возможно стоит создать короткий ДТО для продукта, что-бы посмотреть кратко о продукте через изображение
    }
}
