using System;

namespace NextInpact.Core
{
    public class Constantes
    {
        public const String NEXT_INPACT_URL = "https://m.nextinpact.com";
        public const String NEXT_INPACT_URL_NUM_PAGE = NEXT_INPACT_URL + "/?page=";
        public const String NEXT_INPACT_URL_COMMENTAIRES = NEXT_INPACT_URL + "/comment/";
        public const String AUTHENTIFICATION_URL = NEXT_INPACT_URL + "/Account/LogOn";
        public const String NEXT_INPACT_URL_COMMENTAIRES_PARAM_ARTICLE_ID = "newsId";
        public const String NEXT_INPACT_URL_COMMENTAIRES_PARAM_NUM_PAGE = "page";
        public const int TIMEOUT = 15000;
        public const int NB_COMMENTAIRES_PAR_PAGE = 10;
        public const int NB_ARTICLES_PAR_PAGE = 30;        
        public const String FORMAT_DATE_ARTICLE = "dd/MM/yyyy HH:mm:ss";
        public const String FORMAT_DATE_COMMENTAIRE = "'le' dd/MM/yyyy 'à' HH:mm:ss";
        public const String FORMAT_AFFICHAGE_COMMENTAIRE_DATE_HEURE = FORMAT_DATE_COMMENTAIRE;        
        public const String FORMAT_AFFICHAGE_SECTION_DATE = "dddd dd MMMM yyyy";
        public const String FORMAT_AFFICHAGE_ARTICLE_HEURE = "HH:mm";
        public const String FORMAT_DATE_DERNIER_REFRESH = "dd MMM 'à' HH:mm";
        public const int DB_REFRESH_ID_LISTE_ARTICLES = 0;
        public const int TEXT_SIZE_MICRO = 12;
        public const int TEXT_SIZE_SMALL = 14;
        public const int TEXT_SIZE_MEDIUM = 18;
        public const int MARGE_DROITE_IMAGE = 30;
        public const String AUTHENTIFICATION_USERNAME = "UserName";
        public const String AUTHENTIFICATION_PASSWORD = "Password";
        public const String AUTHENTIFICATION_COOKIE = "inpactstore";
        public const String SCHEME_IFRAME_DRAWABLE = "http://IFRAME_LOCALE/";
        public const String USER_AGENT = "NextInpact (Unofficial) v";
    }

}
