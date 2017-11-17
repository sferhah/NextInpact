namespace NextInpact.Core
{
    public class Constants
    {
        public const string NEXT_INPACT_URL = "https://m.nextinpact.com";
        public const string NEXT_INPACT_URL_NUM_PAGE = NEXT_INPACT_URL + "/?page=";
        public const string NEXT_INPACT_URL_COMMENTAIRES = NEXT_INPACT_URL + "/comment/";
        public const string NEXT_INPACT_URL_COMMENTAIRES_PARAM_ARTICLE_ID = "newsId";
        public const string NEXT_INPACT_URL_COMMENTAIRES_PARAM_NUM_PAGE = "page";
        public const int NB_COMMENTAIRES_PAR_PAGE = 10;
        public const string FORMAT_DATE_ARTICLE = "dd/MM/yyyy HH:mm:ss";
        public const string FORMAT_DATE_COMMENTAIRE = "'le' dd/MM/yyyy 'à' HH:mm:ss";
        public const string FORMAT_AFFICHAGE_COMMENTAIRE_DATE_HEURE = FORMAT_DATE_COMMENTAIRE;        
        public const string FORMAT_AFFICHAGE_SECTION_DATE = "dddd dd MMMM yyyy";
        public const string FORMAT_AFFICHAGE_ARTICLE_HEURE = "HH:mm";
        public const string FORMAT_DATE_DERNIER_REFRESH = "dd MMM 'à' HH:mm";  
        public const string AUTHENTIFICATION_USERNAME = "UserName";
        public const string AUTHENTIFICATION_PASSWORD = "Password";
        public const string AUTHENTIFICATION_COOKIE = "inpactstore";
        public const string SCHEME_IFRAME_DRAWABLE = "http://IFRAME_LOCALE/";
        public const string USER_AGENT = "NextInpact (Unofficial) v";
    }

}
