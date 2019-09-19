using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Salers;
using Nop.Core.Html;
using Nop.Services.Events;

namespace Nop.Services.Salers
{
    /// <summary>
    /// Saler service
    /// </summary>
    public partial class SalerService : ISalerService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Saler> _salerRepository;
        private readonly IRepository<SalerNote> _salerNoteRepository;

        #endregion

        #region Ctor

        public SalerService(IEventPublisher eventPublisher,
            IRepository<Saler> salerRepository,
            IRepository<SalerNote> salerNoteRepository)
        {
            _eventPublisher = eventPublisher;
            _salerRepository = salerRepository;
            _salerNoteRepository = salerNoteRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a saler by saler identifier
        /// </summary>
        /// <param name="salerId">Saler identifier</param>
        /// <returns>Saler</returns>
        public virtual Saler GetSalerById(int salerId)
        {
            if (salerId == 0)
                return null;

            return _salerRepository.GetById(salerId);
        }

        /// <summary>
        /// Delete a saler
        /// </summary>
        /// <param name="saler">Saler</param>
        public virtual void DeleteSaler(Saler saler)
        {
            if (saler == null)
                throw new ArgumentNullException(nameof(saler));

            saler.Deleted = true;
            UpdateSaler(saler);

            //event notification
            _eventPublisher.EntityDeleted(saler);
        }

        /// <summary>
        /// Gets all salers
        /// </summary>
        /// <param name="name">Saler name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Salers</returns>
        public virtual IPagedList<Saler> GetAllSalers(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _salerRepository.Table;
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.Name.Contains(name));
            if (!showHidden)
                query = query.Where(v => v.Active);

            query = query.Where(v => !v.Deleted);
            query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name);

            var salers = new PagedList<Saler>(query, pageIndex, pageSize);
            return salers;
        }

        /// <summary>
        /// Gets salers
        /// </summary>
        /// <param name="salerIds">Saler identifiers</param>
        /// <returns>Salers</returns>
        public virtual IList<Saler> GetSalersByIds(int[] salerIds)
        {
            var query = _salerRepository.Table;
            if (salerIds != null)
                query = query.Where(v => salerIds.Contains(v.Id));

            return query.ToList();
        }

        /// <summary>
        /// Inserts a saler
        /// </summary>
        /// <param name="saler">Saler</param>
        public virtual void InsertSaler(Saler saler)
        {
            if (saler == null)
                throw new ArgumentNullException(nameof(saler));

            _salerRepository.Insert(saler);

            //event notification
            _eventPublisher.EntityInserted(saler);
        }

        /// <summary>
        /// Updates the saler
        /// </summary>
        /// <param name="saler">Saler</param>
        public virtual void UpdateSaler(Saler saler)
        {
            if (saler == null)
                throw new ArgumentNullException(nameof(saler));

            _salerRepository.Update(saler);

            //event notification
            _eventPublisher.EntityUpdated(saler);
        }

        /// <summary>
        /// Gets a saler note
        /// </summary>
        /// <param name="salerNoteId">The saler note identifier</param>
        /// <returns>Saler note</returns>
        public virtual SalerNote GetSalerNoteById(int salerNoteId)
        {
            if (salerNoteId == 0)
                return null;

            return _salerNoteRepository.GetById(salerNoteId);
        }

        /// <summary>
        /// Deletes a saler note
        /// </summary>
        /// <param name="salerNote">The saler note</param>
        public virtual void DeleteSalerNote(SalerNote salerNote)
        {
            if (salerNote == null)
                throw new ArgumentNullException(nameof(salerNote));

            _salerNoteRepository.Delete(salerNote);

            //event notification
            _eventPublisher.EntityDeleted(salerNote);
        }

        /// <summary>
        /// Formats the saler note text
        /// </summary>
        /// <param name="salerNote">Saler note</param>
        /// <returns>Formatted text</returns>
        public virtual string FormatSalerNoteText(SalerNote salerNote)
        {
            if (salerNote == null)
                throw new ArgumentNullException(nameof(salerNote));

            var text = salerNote.Note;

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = HtmlHelper.FormatText(text, false, true, false, false, false, false);

            return text;
        }

        #endregion
    }
}