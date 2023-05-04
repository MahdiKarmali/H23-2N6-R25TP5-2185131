using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public abstract class Chanson : IChanson
    {

        #region Champs

        protected int m_annee;
        protected string m_artiste;
        protected string m_nomFichier;
        protected string m_titre;

        #endregion



        #region Propriétés


        public int Annee
        {
            get { return m_annee; } 
        }


        public string Artiste
        {
            get { return m_artiste; }
        }


        public abstract string Format { get; }


        public string NomFichier
        {
            get { return m_nomFichier;}
        }


        public string Paroles
        {
            get { return LireParoles(new StreamReader(m_nomFichier)); }
        }


        public string Titre
        {
            get { return m_titre; }
        }

        #endregion


        #region Constructeurs

        /// <summary>
        /// Initialise une instance, elle appelle la méthode LireEntete
        /// </summary>
        /// <param name="pNomFichier"></param>
        public Chanson(string pNomFichier)
        {
            m_nomFichier = pNomFichier;
            LireEntete();
        }



        /// <summary>
        /// Initialise les instances, le nom de fichier doit contenir le nom de répertoire , le nom de fichier et son format.
        /// </summary>
        /// <param name="pRepertoire"></param>
        /// <param name="pArtiste"></param>
        /// <param name="pTitre"></param>
        /// <param name="pAnnée"></param>
        public Chanson(string pRepertoire, string pArtiste, string pTitre, int pAnnée)
        {
            m_artiste = pArtiste;
            m_titre = pTitre;
            m_annee = pAnnée;
            m_nomFichier = pRepertoire + m_nomFichier + "." + Format.ToLower();
        }

        #endregion



        #region Méthodes

        

        /// <summary>
        /// Écrit les paroles passées en paramètre dans le fichier de la chanson. Elle doit d’abord écrire l’en-tête ensuite écrire les paroles.
        /// </summary>
        /// <param name="pParoles"></param>
        public void Ecrire(string pParoles)
        {
            StreamWriter objFichier = new StreamWriter(m_nomFichier);

            EcrireEntete(objFichier);
            EcrireParoles(objFichier, pParoles);

            objFichier.Close();
        }

        /// <summary>
        /// Des méthodes abstraites qui permettent d’écrire l’entête et les paroles de la chanson
        /// </summary>
        /// <param name="pobjFichier"></param>
        public abstract void EcrireEntete(StreamWriter pobjFichier);


        public abstract void EcrireParoles(StreamWriter pobjFichier, string pParoles);


        /// <summary>
        /// Des méthodes abstraites à redéfinir dans les classes dérivées
        /// </summary>
        public abstract void LireEntete();


        public abstract string LireParoles(StreamReader pobjFichier);
        

        public void SauterEntete(StreamReader pobjFichier)
        {
            pobjFichier.ReadLine();
            
        }

        #endregion



    }
}
