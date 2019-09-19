using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Salers;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Saler service interface
    /// </summary>
    public partial interface ISalerService
    {
        /// <summary>
        /// Gets a saler by saler identifier
        /// </summary>
        /// <param name="salerId">Saler identifier</param>
        /// <returns>Saler</returns>
        Saler GetSalerById(int salerId);

        /// <summary>
        /// Delete a saler
        /// </summary>
        /// <param name="saler">Saler</param>
        void DeleteSaler(Saler saler);

        /// <summary>
        /// Gets all salers
        /// </summary>
        /// <param name="name">Saler name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Salers</returns>
        IPagedList<Saler> GetAllSalers(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets salers
        /// </summary>
        /// <param name="salerIds">Saler identifiers</param>
        /// <returns>Salers</returns>
        IList<Saler> GetSalersByIds(int[] salerIds);

        /// <summary>
        /// Inserts a saler
        /// </summary>
        /// <param name="saler">Saler</param>
        void InsertSaler(Saler saler);

        /// <summary>
        /// Updates the saler
        /// </summary>
        /// <param name="saler">Saler</param>
        void UpdateSaler(Saler saler);

        /// <summary>
        /// Gets a saler note
        /// </summary>
        /// <param name="salerNoteId">The saler note identifier</param>
        /// <returns>Saler note</returns>
        SalerNote GetSalerNoteById(int salerNoteId);

        /// <summary>
        /// Deletes a saler note
        /// </summary>
        /// <param name="salerNote">The saler note</param>
        void DeleteSalerNote(SalerNote salerNote);

        /// <summary>
        /// Formats the saler note text
        /// </summary>
        /// <param name="salerNote">Saler note</param>
        /// <returns>Formatted text</returns>
        string FormatSalerNoteText(SalerNote salerNote);
    }
}