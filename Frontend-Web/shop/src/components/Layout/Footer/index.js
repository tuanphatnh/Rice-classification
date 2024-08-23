import classNames from 'classnames/bind';
import styles from './Footer.module.scss';
import { Link } from 'react-router-dom';

import fbIcon from '~/assets/icon/facebook.png';
import ytIcon from '~/assets/icon/youtube.png';
import chplayIcon from '~/assets/icon/google-play.png';
import apstoreIcon from '~/assets/icon/apple-logo.png';
import arrowIcon from '~/assets/icon/right-arrow.png';

const cx = classNames.bind(styles);

function Footer() {
    return (
        <div className={cx('wrap')}>
            <div className={cx('container')}>
                <div className={cx('content')}>
                    <h1 className={cx('heading')}>Tải ứng dụng</h1>
                    <div className={cx('list')}>
                        <Link>
                            <div className={cx('item-icon') + ' flex items-center mb-5'}>
                                <img className={cx('icon')} src={apstoreIcon} alt="" />
                                <span className="px-3">Apple Store</span>
                            </div>
                        </Link>
                        <Link>
                            <div className={cx('item-icon') + ' flex items-center mb-5'}>
                                <img className={cx('icon')} src={chplayIcon} alt="" />
                                <span className="px-3">Google Play</span>
                            </div>
                        </Link>
                    </div>
                </div>
                <div className={cx('content')}>
                    <h1 className={cx('heading')}>Hổ trợ khách hàng</h1>
                    <div className={cx('list')}>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> Trung tâm trợ giúp
                        </p>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> An toàn mua bán
                        </p>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> Liên hệ hỗ trợ
                        </p>
                    </div>
                </div>
                <div className={cx('content')}>
                    <h1 className={cx('heading')}>Về trang web</h1>
                    <div className={cx('list')}>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> Giới thiệu trang web
                        </p>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> Giải quyết tranh chấp
                        </p>
                        <p className={cx('item')}>
                            <img className={cx('icon-arrow')} alt="" src={arrowIcon}></img> Chính sách bảo mật
                        </p>
                    </div>
                </div>
                <div className={cx('content')}>
                    <h1 className={cx('heading')}>Liên kết</h1>
                    <div className={cx('list')}>
                        <Link>
                            <div className="flex items-center">
                                <img className={cx('icon')} src={fbIcon} alt="" />
                                <span className="px-3">Facebook</span>
                            </div>
                        </Link>
                        <Link>
                            <div className="flex items-center my-4">
                                <img className={cx('icon')} src={ytIcon} alt="" />
                                <span className="px-3">Youtube</span>
                            </div>
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Footer;
