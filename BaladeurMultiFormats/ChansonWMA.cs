using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public class ChansonWMA : Chanson
    {

        #region Champs

        private int m_codage;

        #endregion

        #region Propriétés

        public override string Format
        { get; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pNomFichier"></param>
        public ChansonWMA(string pNomFichier)
            :base(pNomFichier)
        {
            Format = "WMA";
            Random objRandom = new Random();
            m_codage = objRandom.Next(3, 15);
        }

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pRepertoire"></param>
        /// <param name="pArtiste"></param>
        /// <param name="pTitre"></param>
        /// <param name="pAnnée"></param>
        public ChansonWMA(string pRepertoire, string pArtiste, string pTitre, int pAnnée)
            : base(pRepertoire, pArtiste, pTitre, pAnnée)
        {
            Format = "WMA";
            Random objRandom = new Random();
            m_codage = objRandom.Next(3, 15);
        }

        #endregion


        #region Méthodes

        /// <summary>
        /// Génère un numéro de codage entre 3 et 15 et écrit une ligne dans le fichier passé en paramètre.
        /// Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson.
        /// </summary>
        /// <param name="pobjFichier"></param>
        public override void EcrireEntete(StreamWriter pobjFichier)
        {
            pobjFichier.WriteLine(m_codage + " / " + Annee + " / " + Titre + " / " + Artiste);
        }


        /// <summary>
        /// Encode les paroles reçues en paramètre au format WMA, ensuite écrit ses paraoles encodées dans le fichier passé en paramètre.
        /// </summary>
        /// <param name="pobjFichier"></param>
        /// <param name="pParoles"></param>
        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            pobjFichier.Write(OutilsFormats.EncoderWMA(pParoles, m_codage));
        }


        /// <summary>
        /// Lit la premiere ligne du fichier de la chanson et initialise les champs de la chanson (codage,titre, artiste et année de création de la chanson)
        /// </summary>
        public override void LireEntete()
        {
            StreamReader objFichier = new StreamReader(m_nomFichier);

            string uneLigne = objFichier.ReadLine();
            string[] tabLigne = uneLigne.Split(':');

            m_codage = int.Parse(tabLigne[0].Trim());
            m_annee = int.Parse(tabLigne[1].Trim());
            m_titre = tabLigne[2].Trim();
            m_artiste = tabLigne[3].Trim();

            objFichier.Close();
        }


        /// <summary>
        /// Récupère les paroles de la chanson à partir du fichier passé en paramètre, les décode selon le format WMA et les retourne.
        /// </summary>
        /// <param name="pobjFichier"></param>
        /// <returns></returns>
        public override string LireParoles(StreamReader pobjFichier)
        {
            SauterEntete(pobjFichier);

            string lesParoles = pobjFichier.ReadToEnd();

            pobjFichier.Close();

            return OutilsFormats.DecoderWMA(lesParoles, m_codage);
        }

        #endregion
    }
}
