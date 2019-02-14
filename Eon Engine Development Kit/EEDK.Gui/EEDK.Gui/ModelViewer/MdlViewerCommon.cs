/* Created: 26/01/2015
 * Last Updated: 26/01/2015
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace EEDK.Gui.ModelViewer
{
    /// <summary>
    /// Used to define common aspects between
    /// the ModelViewer and it's MenuScreen.
    /// </summary>
    public static class MdlViewerCommon
    {
       static CommonPass common = new CommonPass();

       public static CommonPass Common
       {
           get { return common; }
       }
    }
}
