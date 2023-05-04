using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public class ChansonMP3 : Chanson
    {
        #region Propriétés

        public override string Format
        {
            get;
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pNomFichier"></param>
        public ChansonMP3(string pNomFichier)
            :base(pNomFichier)
        {
            Format = "MP3";
        }

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pRepertoire"></param>
        /// <param name="pArtiste"></param>
        /// <param name="pTitre"></param>
        /// <param name="pAnnée"></param>
        public ChansonMP3(string pRepertoire, string pArtiste, string pTitre, int pAnnée)
            : base(pRepertoire, pArtiste, pTitre, pAnnée)
        {
            Format = "MP3";
        }

        #endregion


        #region Méthodes


        /// <summary>
        /// Écrit une ligne dans le fichier passé en paramètre. Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson 
        /// </summary>
        /// <param name="pobjFichier"></param>
        public override void EcrireEntete(StreamWriter pobjFichier)
        {
            pobjFichier.WriteLine(Artiste + " | " + Annee + " | " + Titre);
        }


        /// <summary>
        /// Encode les paroles recue en paramètre au format MP3, ensuite écrit ses paraoles encodées dans le fichier passé en paramètre.
        /// </summary>
        /// <param name="pobjFichier"></param>
        /// <param name="pParoles"></param>
        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            pobjFichier.Write(OutilsFormats.EncoderMP3(pParoles));
        }


        /// <summary>
        /// Lit la premiere ligne du fichier de la chanson et initialise les champs de la chanson (artiste, année de création et titre).
        /// La méthode Trim() est utilisé pour enlever les espaces vides au début et à la fin d’une chaine de caractètres.
        /// </summary>
        public override void LireEntete()
        {
            StreamReader objFichier = new StreamReader(m_nomFichier);

            string ligneLu = objFichier.ReadLine();
            string[] tabInfo = ligneLu.Split(':');

            m_titre = tabInfo[0].Trim();
            m_artiste = tabInfo[1].Trim();
            m_annee = int.Parse(tabInfo[2].Trim());

            objFichier.Close();
        }


        /// <summary>
        /// Récupèere les paroles de la chanson à partir du fichier passé en paramètre, les décode en format MP3 et les retourne.
        /// </summary>
        /// <param name="pobjFichier"></param>
        /// <returns></returns>
        public override string LireParoles(StreamReader pobjFichier)
        {
            string uneLigne = pobjFichier.ReadToEnd();
            pobjFichier.Close();

            return OutilsFormats.DecoderMP3(uneLigne);
        }

        #endregion
    }
}
