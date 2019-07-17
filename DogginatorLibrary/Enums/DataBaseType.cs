/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DataBaseType.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */


namespace de.rietrob.dogginator_product.DogginatorLibrary.Enums
{
    /// <summary>
    /// Way to store Data Defaullt: SQLite
    /// </summary>
    public enum DataBaseType
    {
        SQLite,
        MySQL,
        TextFile,
        XML
    }
}
