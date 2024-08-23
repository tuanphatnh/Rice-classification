import classNames from 'classnames/bind';
import styles from './Home.module.scss';
import { Link } from 'react-router-dom';

import mobile from '~/assets/icon/mobile.png';
import tablet from '~/assets/icon/tablet.png';
import watch from '~/assets/icon/smart-watch.png';
import laptop from '~/assets/icon/laptop.png';
import Slider from '~/components/HomePage/Slider';
import CardNews from '~/components/Global/CardNews';
import { useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import { useCookies } from 'react-cookie';
import mobilePhone from '~/assets/images/app-development.png';
import axios from 'axios';

const cx = classNames.bind(styles);

function Home() {
    const [cookies, setCookies] = useCookies(['user']);
    const [listProduct, setListProduct] = useState([]);

    useEffect(() => {
        axios
            .get('https://localhost:44352/api/Product/getAll', {
                params: {
                    page: 0,
                    size: 8,
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    setListProduct(res.data.productList);
                }
            })
            .catch((err) => console.log(err));
    }, []);
    return (
        <div className={cx('wrap')}>
            <div className={cx('container')}>
                <div className="container">
                    <div className={cx('slide')}>
                        <Slider />
                    </div>
                    <div className={cx('list-cate')}>
                    <Link to="/news?category=3">
                            <div className={cx('cate-item', 'item-1')}>
                                <img src={mobile} alt="" />
                                <span>Phone</span>
                            </div>
                        </Link>
                        <Link to="/news?category=4">
                            <div className={cx('cate-item', 'item-2')}>
                                <img src={laptop} alt="" />
                                <span>Laptop</span>
                            </div>
                        </Link>
                        <Link to="/news">
                            <div className={cx('cate-item', 'item-4')}>
                                <img src={mobilePhone} alt="" />
                                <span>ALL</span>
                            </div>
                        </Link>
                        <Link to="/news?category=5">
                            <div className={cx('cate-item', 'item-3')}>
                                <img src={watch} alt="" />
                                <span>Watch</span>
                            </div>
                        </Link>
                        <Link to="/news?category=6">
                            <div className={cx('cate-item', 'item-4')}>
                                <img src={tablet} alt="" />
                                <span>Tablet</span>
                            </div>
                        </Link>
                    </div>
                    <div>
                        <h1 className={cx('heading-1') + ' text-center'}>NEW POSTS</h1>
                        <div className={cx('list')}>
                            <div className="grid grid-cols-3 gap-x-8 gap-y-12">
                                {listProduct.map((product, index) => (
                                    <div className={cx('item')}>
                                        <CardNews key={index} product={product} />
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Home;
