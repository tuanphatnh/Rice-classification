import classNames from 'classnames/bind';
import styles from './CardNews.module.scss';
import { useEffect, useState } from 'react';

import { NumberVND } from '~/functions';
import img from '~/assets/images/iphone-14.jpg';
import chatIcon from '~/assets/icon/messenger.png';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLocationDot, faHeart as faHeartSolid, faEye, faCircleInfo } from '@fortawesome/free-solid-svg-icons';
import { faHeart as faHeartRegular } from '@fortawesome/free-regular-svg-icons';
import { Modal, Tooltip, Image } from 'antd';
import { Link } from 'react-router-dom';
import axios from 'axios';

const cx = classNames.bind(styles);

function CardNews({ product }) {
    const [isHeart, setIsHeart] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [author, setAuthor] = useState(null);
    const [address, setAdress] = useState(null);
    const [color, setColor] = useState(null);
    const showModal = (e) => {
        e.stopPropagation();
        setIsModalOpen(true);
    };

    useEffect(() => {
        axios
            .get(`https://localhost:44352/api/ShippingAddress/getById/${product.userId}`)
            .then((res) => {
                if (res.status === 200) {
                    setAdress(res.data);
                }
            })
            .catch((err) => console.log(err));

        axios
            .get(`https://localhost:44352/api/Color/getById/${product.colorId}`)
            .then((res) => {
                if (res.status === 200) {
                    setColor(res.data.colorName);
                }
            })
            .catch((err) => console.log(err));
    }, [product]);

    return (
        <div className={cx('wrap')}>
            <img src={product?.images[0].url} alt="" />
            <div className={cx('icon-heart')}>
                {isHeart ? (
                    <FontAwesomeIcon icon={faHeartSolid} onClick={() => setIsHeart(false)} />
                ) : (
                    <FontAwesomeIcon icon={faHeartRegular} onClick={() => setIsHeart(true)} />
                )}
            </div>
            <div className={cx('list-icon')}>
                <Tooltip placement="top" title={'Xem nhanh'}>
                    <FontAwesomeIcon className={cx('icon')} icon={faEye} onClick={(e) => showModal(e)} />
                </Tooltip>

                <Tooltip placement="top" title={'Xem sản phẩm'}>
                    <Link to={`/detail/${product.id}`}>
                        <FontAwesomeIcon className={cx('icon')} icon={faCircleInfo} />
                    </Link>
                </Tooltip>
            </div>
            <div className={cx('content')}>
                <h2>{product.name}</h2>
                <h3>{NumberVND(product.price)}đ</h3>
                <div className={cx('location')}>
                    <FontAwesomeIcon className={cx('icon')} icon={faLocationDot} />
                    <p className="px-3">Cần Thơ</p>
                </div>
            </div>
            <Modal
                title="Chi Tiết Sản Phẩm"
                width={800}
                open={isModalOpen}
                footer={null}
                onCancel={() => setIsModalOpen(false)}
            >
                <div className={cx('body')}>
                    <div className="grid grid-cols-2 gap-4">
                        <div className={cx('img')}>
                            <Image width={300} src={product?.images[0].url} />
                        </div>
                        <div className={cx('content')}>
                            <h1>{product.name}</h1>
                            <div className="px-10">
                                <div className="grid grid-cols-2 gap-5">
                                    <div className="col-span">
                                        <h2>
                                            {/* <b>Loại: </b> {product.category.name} */}
                                        </h2>
                                        <h2>
                                            <b>Địa chỉ: </b> Cần Thơ
                                        </h2>

                                        <h2>
                                            <b>SĐT: </b> <span>{product.phoneNumber}</span>
                                        </h2>
                                    </div>
                                    <div className="col-span">
                                        <h2>
                                            <b>Giá:</b> <span className="color">{NumberVND(product.price)}đ</span>
                                        </h2>
                                        <h2>
                                            {/* <b>Hãng:</b> <span>{product.brand.name}</span> */}
                                        </h2>
                                        <h2>
                                            <b>Màu sắc:</b> <span>{color}</span>
                                        </h2>
                                    </div>
                                </div>
                                <div className="flex">
                                    <div className="flex justify-center p-6 mt-5">
                                        <button className={cx('send')}>
                                            Nhắn tin <img className="mx-3" width={24} src={chatIcon} alt="" />
                                        </button>
                                    </div>
                                    <Link to={`/detail/${product.id}`}>
                                        <div className="flex justify-center p-6 mt-5">
                                            <button className={cx('send')}>Xem sản phẩm</button>
                                        </div>
                                    </Link>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="p-6">
                        <b>Mô tả</b>
                        <p className={cx('description')}>{product?.description}</p>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default CardNews;
