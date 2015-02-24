using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Import.ArmA3
{
    internal class ItemListParser<TItemType> : IParser<List<TItemType>>
    {
        private readonly Regex _classRegex = new Regex(@"class\s+(?<class>\w+)", RegexOptions.Compiled);
        private readonly Regex _itemCountRegex = new Regex(@"items\=(?<itemCount>\d+)", RegexOptions.Compiled);

        private readonly IItemParserFactory<TItemType> _itemParserFactory;
        private readonly string _listTypeName;

        private int _itemCount;

        public ItemListParser(IItemParserFactory<TItemType> itemParserFactory, string listTypeName)
        {
            _itemParserFactory = itemParserFactory;
            _listTypeName = listTypeName;
        }

        public bool IsCorrectContext(SqmContext context)
        {
            var isCorrectListElement = false;

            context.MatchHeader(_classRegex, match =>
                {
                    isCorrectListElement = match.Groups["class"].Value == _listTypeName;
                });

            return isCorrectListElement;
        }

        public List<TItemType> ParseContext(SqmContext context)
        {
            _itemCount = 0;
            var itemParser = _itemParserFactory.CreateParser();

            foreach (var line in context.Lines.Where(line => line.IsMatch(_itemCountRegex)))
            {
                line.Match(_itemCountRegex, SetItemCount);
            }

            var itemList = (from subContext in context.SubContexts where itemParser.IsCorrectContext(subContext) select itemParser.ParseContext(subContext)).ToList();

            if (_itemCount != itemList.Count)
            {
                throw new SqmParseException("Declared item count does not match actual item count.\n" +
                                            "Declared: " + _itemCount + "\n" +
                                            "Actual: " + itemList.Count);
            }
            
            if(_itemCount == 0)
            {
                throw new SqmParseException("Item list cannot be empty");
            }

            return itemList;
        }

        private void SetItemCount(Match match)
        {
            var itemCountGroup = match.Groups["itemCount"];
            _itemCount = Convert.ToInt32(itemCountGroup.Value);
        }
    }
}
