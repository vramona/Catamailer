using System.IO;
using System.Text;

namespace AiDocGenerator.Helpers
{
    /// <summary>
    /// Utilitaire pour manipuler et mettre à jour les fichiers de table des matières (toc.yml) de DocFX.
    /// </summary>
    public static class TocModifier
    {
        /// <summary>
        /// Injecte une nouvelle entrée dans un fichier toc.yml si elle n'existe pas déjà.
        /// </summary>
        /// <param name="tocPath">Le chemin absolu vers le fichier toc.yml.</param>
        /// <param name="name">Le nom affiché dans la table des matières.</param>
        /// <param name="href">Le lien relatif (href) de l'entrée.</param>
        public static void InjectEntryIfNeeded(string tocPath, string name, string href)
        {
            if (File.Exists(tocPath))
            {
                var content = File.ReadAllText(tocPath);

                // CORRECTION DU BUG : On vérifie la présence exacte du lien indépendamment du tiret YAML
                if (!content.Contains($"href: {href}"))
                {
                    var utf8WithBom = new UTF8Encoding(true);
                    File.AppendAllText(tocPath, $"\n- name: {name}\n  href: {href}\n", utf8WithBom);
                }
            }
        }
    }
}