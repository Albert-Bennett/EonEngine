/* Created 29/09/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

using Eon.Game2D.TileEngine.Management;
using Eon.Helpers;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;
using System.Xml;

namespace Eon.ContentPipeline.Tile
{
    /// <summary>
    /// Used to process .TileFiles
    /// </summary>
    [ContentProcessor(DisplayName = "Tile Map Processor - Eon Framework")]
    public class TileMapProcessor : ContentProcessor<XmlDocument, TileMapDeffination>
    {
        XmlDocument input;

        public override TileMapDeffination Process(XmlDocument input, ContentProcessorContext context)
        {
            TileMapDeffination output = new TileMapDeffination();
            this.input = input;

            if (input.GetElementsByTagName("TileMap").Count > 0)
            {
                if (input.GetElementsByTagName("Info").Count > 0)
                {
                    int count = 0;
                    bool postRender = false;

                    foreach (XmlNode node in input.GetElementsByTagName("Info")[0].ChildNodes)
                        switch (node.Name)
                        {
                            case "TileLayers":
                                int.TryParse(node.InnerText, out count);
                                break;

                            case "PostRender":
                                bool.TryParse(node.InnerText, out postRender);
                                break;
                        }

                    output.PostRender = postRender;
                    output.TileLayers = new TileLayerDeffination[0];

                    for (int i = 0; i < count; i++)
                        output.TileLayers = ArrayHelper.AddItem<TileLayerDeffination>(ProcessTileLayer(i), output.TileLayers);
                }
            }

            return output;
        }

        TileLayerDeffination ProcessTileLayer(int index)
        {
            TileLayerDeffination output = new TileLayerDeffination();

            string tag = "TileLayer" + index;

            if (input.GetElementsByTagName(tag).Count > 0)
                foreach (XmlNode node in input.GetElementsByTagName(tag)[0].ChildNodes)
                {
                    List<List<int>> tiles = new List<List<int>>();

                    switch (node.Name)
                    {
                        case "Columns":
                            output.Columns = int.Parse(node.InnerText);
                            break;

                        case "Rows":
                            output.Rows = int.Parse(node.InnerText);
                            break;

                        case "DrawLayer":
                            output.DrawLayer = int.Parse(node.InnerText);
                            break;

                        case "TotalTiles":
                            output.TotalTileImages = int.Parse(node.InnerText);
                            break;

                        case "TileSize":
                            output.TileSize = SerializationHelper.GetPoint(node.InnerText);
                            break;

                        case "TileOffset":
                            output.TileOffset = SerializationHelper.GetPoint(node.InnerText);
                            break;

                        case "TileSheetFilepath":
                            output.TileSheetFilepath = node.InnerText;
                            break;
                    }

                    TypeHelper.ProcessListOfList(ref input, tag + "Tiles", out tiles);


                    output.Tiles = tiles;
                }

            return output;
        }
    }
}