using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public class ChansonAAC : Chanson
    {
        #region Propriété

        public string Format { get; }

        #endregion


        #region Constructeurs

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pNomFichier"></param>
        public ChansonAAC(string pNomFichier)
            :base(pNomFichier)
        {
            Format = "AAC";
        }

        /// <summary>
        /// Initialise une instance avec les données passées en paramètres en appelant le constructeur de la classe de base.
        /// </summary>
        /// <param name="pRepertoire"></param>
        /// <param name="pArtiste"></param>
        /// <param name="pTitre"></param>
        /// <param name="pAnnée"></param>
        public ChansonAAC(string pRepertoire, string pArtiste, string pTitre, int pAnnée)
            :base(pRepertoire, pArtiste, pTitre, pAnnée)
        {
            Format = "AAC";
        }

        #endregion


        #region Méthodes

        /// <summary>
        /// Écrit une ligne dans le fichier passé en paramètre. Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson.
        /// </summary>
        /// <param name="pobjFichier"></param>
        public override void EcrireEntete(StreamWriter pobjFichier)
        {
            pobjFichier.WriteLine("Titre = " + Titre + "Artiste = " + Artiste + "Année = " + Annee);
        }

        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            pobjFichier.Write(OutilsFormats.EncoderAAC(pParoles));
        }


        /// <summary>
        /// Lit la premiere ligne du fichier de la chanson et initialise les champs de la chanson (titre, artiste et année de création de la chanson). 
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

        public override string LireParoles(StreamReader pobjFichier)
        {
            string ligneLu = pobjFichier.ReadToEnd();

            pobjFichier.Close();
            
            return OutilsFormats.DecoderAAC(ligneLu);
        }

        #endregion


    }
}
