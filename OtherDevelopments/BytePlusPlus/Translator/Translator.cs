using Yandex = YandexLinguistics.NET;
namespace Translator
{
    class Translator
    {
        Yandex.Translator translator;
        const string translatorKey = "trnsl.1.1.20190410T104358Z.0fbbc108134f78a4.e663cf452013444d6daf5a56e031238a46105083";

        public Translator()
        {
            translator = new Yandex.Translator(translatorKey);
        }

        public Yandex.LangPair GetLangPair(string inputLang, string outputLang)
        {
            Yandex.LangPair langPair = new Yandex.LangPair();
            switch (inputLang)
            {
                case "Русский":
                    langPair.InputLang = Yandex.Lang.Ru;
                    break;
                case "Английский":
                    langPair.InputLang = Yandex.Lang.En;
                    break;
                case "Французский":
                    langPair.InputLang = Yandex.Lang.Fr;
                    break;
                default:
                    break;
            }
            switch (outputLang)
            {
                case "Русский":
                    langPair.OutputLang = Yandex.Lang.Ru;
                    break;
                case "Английский":
                    langPair.OutputLang = Yandex.Lang.En;
                    break;
                case "Французский":
                    langPair.OutputLang = Yandex.Lang.Fr;
                    break;
                default:
                    break;
            }
            return langPair;
        }

        public string Translate(string input, Yandex.LangPair langPair)
        {
            return translator.Translate(input, langPair).Text;
        }
    }
}
