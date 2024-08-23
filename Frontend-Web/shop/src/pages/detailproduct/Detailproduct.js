import React, { useEffect, useRef, useState } from 'react';
import '../detailproduct/Detailproduct.css';
import {
    StarOutlined,
    MessageOutlined,
    HeartOutlined,
    InfoCircleOutlined,
    PhoneOutlined,
    WechatOutlined,
    TagOutlined,
    DatabaseOutlined,
    FileProtectOutlined,
    HddOutlined,
    DesktopOutlined,
    GlobalOutlined,
    FileTextOutlined,
    ShopOutlined,
    HeartFilled,
} from '@ant-design/icons';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import { useCookies } from 'react-cookie';

const Detailproduct = () => {
    const [selectedImg, setSelectedImg] = useState(0);
    const [news, setNews] = useState(false);

    const toggle = () => {
        setNews(!news);
    };

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    //get Product
    const param = useParams();
    const [prod, setProd] = useState({});
    const [userInfo, setUserInfo] = useState({});
    const userId = prod.userId;

    useEffect(() => {
        axios.get(`https://localhost:44352/api/Product/getById/${param.id}`).then((res) => setProd(res.data.product));
        axios
            .get(`https://localhost:44352/api/User/getProfile?userId=${userId}`)
            .then((res) => {
                if (res.status === 200) {
                    setUserInfo(res.data);
                }
            })
            .catch((err) => console.log(err));
    }, [param.id, userId]);

    return (
        <div style={{}}>
            <div className="card-wrapper">
                <div className="card">
                    {/* left */}
                    <div className="content">
                        <div className="img-display">
                            <div className="img-showcase">
                                <img src="" alt={selectedImg} />
                            </div>
                        </div>
                        <div className="img-select">
                            {prod.images?.map((img, index) => {
                                return (
                                    <div className="img-item">
                                        <a href="#" data-id="1" onClick={(e) => setSelectedImg({ index })}>
                                            <img src={img.url} alt={index} />
                                        </a>
                                    </div>
                                );
                            })}
                        </div>
                        <div style={{ height: 'auto', margin: '.5rem 1rem' }}>
                            <h1 className="title-product"></h1>
                            <div className="product-price2">
                                <div className="price">{prod.price} d</div>
                                <div className="saveNews" onClick={() => toggle(news)}>
                                    {news ? 'Lưu tin' : 'Đã lưu'} {news ? <HeartOutlined /> : <HeartFilled />}
                                </div>
                            </div>
                            <div className="product-description">
                                <div className="description">
                                    <TagOutlined style={{ marginRight: '0.5rem' }} /> Hãng:
                                </div>
                                <div className="description">
                                    <TagOutlined style={{ marginRight: '0.5rem' }} /> Dòng máy:
                                </div>
                                <div className="description">
                                    <DatabaseOutlined style={{ marginRight: '0.5rem' }} /> Tình trạng:
                                </div>
                                <div className="description">
                                    <FileProtectOutlined style={{ marginRight: '0.5rem' }} /> Chính sách bảo:
                                </div>
                                <div className="description">
                                    <HddOutlined style={{ marginRight: '0.5rem' }} /> Dung lượng:
                                </div>
                                <div className="description">
                                    <DesktopOutlined style={{ marginRight: '0.5rem' }} /> Kích cỡ màng hình:
                                </div>
                                <div className="description">
                                    <GlobalOutlined style={{ marginRight: '0.5rem' }} /> Xuất sứ:
                                </div>
                                <div className="description">
                                    <FileTextOutlined style={{ marginRight: '0.5rem' }} /> Thông tin sử dụng:
                                </div>
                            </div>
                            <div className="location">
                                <strong>Khu vực</strong>
                                <div className="address">
                                    <InfoCircleOutlined />{' '}
                                    <span>855/9, Hẻm 855 Quang Trung, Phường 12, Quận Gò Vấp, Tp Hồ Chí Minh</span>
                                </div>
                            </div>
                            <div className="IntersectBox ">
                                <strong>Chia sẻ bài đăng này cho bạn bè</strong>
                                <div className="social-link">
                                    <a href="#">
                                        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJE8vBPFmLcAKfgR4-l3mPZ0LRrkUfSvhG1w&usqp=CAU" />
                                    </a>
                                    <a href="#">
                                        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR3Km32bHnWZYdPZhMR3YPEd3EiilGH3WQhdQ&usqp=CAU" />
                                    </a>
                                    <a href="#">
                                        <img src="data:image/png;base64,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" />
                                    </a>
                                </div>
                            </div>
                            <div className="report">
                                <em>
                                    Tin đăng này đã được kiểm duyệt. Nếu gặp vấn đề, vui lòng báo cáo tin đăng hoặc liên
                                    hệ CSKH để được giúp đỡ
                                </em>
                                <div className="report-btn">
                                    <button className="report-btn1">Báo tin không hợp lệ</button>
                                    <button className="report-btn2">Báo tin đã bán</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* right */}
                    <div className="site-bar">
                        <div className="card-profile">
                            <div className="profile">
                                <div className="test">
                                    <div className="profile-title">
                                        <div className="avatar">
                                            <img src={userInfo.avatarUrl} alt={userInfo.firstName} />
                                        </div>
                                        <div className="name">
                                            <div className="p-name">{`${userInfo.username}`}</div>
                                            <div>
                                                <Link to={`/user?id=${userInfo.id}`}>
                                                    <button className="btn">Xem cửa hàng</button>
                                                </Link>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="partner">
                                    <div className="partner-name">
                                        <span>Đối tác</span> <h3>Là đối tác của chợ tốt</h3>
                                    </div>
                                    <div className="partner-description">
                                        Cam kết hàng đúng mô tả, bảo hành ít nhất 3 tháng, hỗ trợ đổi trả.{' '}
                                    </div>
                                </div>
                                <div className="btn-buy">mua ngay - đặt cọc</div>
                                <div className="contact">
                                    <div className="contact-wrapper">
                                        <div className="content">
                                            <span>
                                                <WechatOutlined />
                                            </span>
                                            <span>&nbsp;&nbsp;</span>
                                            <span>chat với người bán</span>
                                        </div>
                                    </div>
                                </div>
                                <div className="contact">
                                    <div className="contact-wrapper">
                                        <div className="content">
                                            <span>
                                                <PhoneOutlined />
                                            </span>
                                            <span>&nbsp;&nbsp;</span>
                                            <span>0794994999</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="card-footer">
                <div className="card-top">
                    <h4>Tin rao khác của SHOP</h4>
                    <div className="all">
                        <span>{'Xem tất cả >'}</span>
                    </div>
                </div>

                <div style={{ boxSizing: 'border-box' }}>
                    <div className="card-bot">
                        <div className="card-rowContent">
                            <div className="card-item">
                                {/* list item */}
                                {userInfo.products?.map((p) => {
                                    return (
                                        <div className="item" key={p.id}>
                                            <div className="item-img">
                                                <img src="https://cdn.tgdd.vn/Products/Images/42/153856/iphone-11-trang-1-2-org.jpg" />
                                            </div>
                                            <div className="item-caption">
                                                <div className="item-title">{p.name}</div>
                                                <div className="item-price">{p.price} d</div>
                                            </div>
                                            <div className="item-footer">
                                                <div>
                                                    <ShopOutlined />
                                                </div>
                                                <div className="item-time">
                                                    <span>{p.createdDate}</span>
                                                </div>
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Detailproduct;
