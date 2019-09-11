using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Weixin;

using Nop.Services.Events;


namespace Nop.Services.Weixin
{
    /// <summary>
    /// WxQrImage service
    /// </summary>
    public partial class WxQrImageService : IWxQrImageService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WxQrImage> _wxQrImageRepository;

        #endregion

        #region Ctor

        public WxQrImageService(IEventPublisher eventPublisher,
            IRepository<WxQrImage> wxQrImageRepository)
        {
            _eventPublisher = eventPublisher;
            _wxQrImageRepository = wxQrImageRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an WxQrImage by WxQrImage identifier
        /// </summary>
        /// <param name="WxQrImage">WxQrImage identifier</param>
        /// <returns>Affiliate</returns>
        public virtual WxQrImage GetWxQrImageById(int wxQrImageId)
        {
            if (wxQrImageId == 0)
                return null;

            return _wxQrImageRepository.GetById(wxQrImageId);
        }

        /// <summary>
        /// Inserts an WxQrImage
        /// </summary>
        /// <param name="WxQrImage">WxQrImage</param>
        public virtual void InsertWxQrImage(WxQrImage wxQrImage)
        {
            if (wxQrImage == null)
                throw new ArgumentNullException(nameof(wxQrImage));

            _wxQrImageRepository.Insert(wxQrImage);

            //event notification
            _eventPublisher.EntityInserted(wxQrImage);
        }

        /// <summary>
        /// Updates the WxQrImage
        /// </summary>
        /// <param name="WxQrImage">WxQrImage</param>
        public virtual void UpdateWxQrImage(WxQrImage wxQrImage)
        {
            if (wxQrImage == null)
                throw new ArgumentNullException(nameof(wxQrImage));

            _wxQrImageRepository.Update(wxQrImage);

            //event notification
            _eventPublisher.EntityUpdated(wxQrImage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="WxQrImage"></param>
        /// <param name="fromDB">是否从数据库删除</param>
        public virtual void DeleteWxQrImage(WxQrImage wxQrImage, bool fromDB = false)
        {
            if (wxQrImage == null)
                throw new ArgumentNullException(nameof(wxQrImage));

            if (fromDB)
            {
                _wxQrImageRepository.Delete(wxQrImage);
            }
            else
            {
                wxQrImage.Deleted = true;
                UpdateWxQrImage(wxQrImage);
            }

            //event notification
            _eventPublisher.EntityDeleted(wxQrImage);
        }


        public virtual IPagedList<WxQrImage> GetWxQrImages(
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _wxQrImageRepository.Table;

            query = query.OrderByDescending(a => a.Id);

            var wxQrImages = new PagedList<WxQrImage>(query, pageIndex, pageSize);
            return wxQrImages;
        }

        #endregion
    }
}