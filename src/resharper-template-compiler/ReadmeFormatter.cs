﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CitizenMatt.ReSharper.TemplateCompiler
{
    public class ReadmeFormatter
    {
        private readonly TextWriter writer;

        public ReadmeFormatter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void FormatTemplates(TemplateStore templates)
        {
            var templatesByCategory = new MultiValueDictionary<string, Template>();
            foreach (var template in templates.Templates)
            {
                foreach (var category in template.Categories)
                    templatesByCategory.Add(category, template);
            }

            writer.WriteLine("# Templates");
            writer.WriteLine();

            if (templatesByCategory.Count == 0)
                FormatTemplates(templates.Templates);

            foreach (var category in templatesByCategory.Keys.OrderBy(s => s))
            {
                // TODO: Perhaps split this down into template type - Live, Surround or File
                writer.WriteLine("## {0}", category);
                FormatTemplates(templatesByCategory[category]);
            }
        }

        private void FormatTemplates(IEnumerable<Template> templates)
        {
            writer.WriteLine();
            writer.WriteLine("Shortcut | Description");
            writer.WriteLine("---------|------------");
            foreach (var template in templates.OrderBy(t => t.Shortcut))
                writer.WriteLine("[{0}]({0}.md) | {1}", template.Shortcut, template.Description);

            writer.WriteLine();
        }
    }
}