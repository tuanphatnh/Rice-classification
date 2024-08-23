import classNames from 'classnames/bind';
import styles from './news.module.scss';

import { useEffect, useState } from 'react';
import { Select, Slider, Input, Pagination } from 'antd';
import { NumberVND } from '~/functions';
import CardNews from '~/components/Global/CardNews';

import searchIcon from '~/assets/icon/magnifying-glass.png';
import { useCookies } from 'react-cookie';
import axios from 'axios';
import { list } from 'postcss';
import { useParams, useSearchParams } from 'react-router-dom';

const cx = classNames.bind(styles);

function News() {
    const [priceValue, setPriceValue] = useState([0, 10000000]);
    const [cookies, setCookies] = useCookies(['user']);
    const [listProduct, setListProduct] = useState([]);
    const [params, setSearch] = useSearchParams();

    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    useEffect(() => {
        if (params.get('search')) {
            const data = {
                keyWord: params.get('search'),
                categoryId: 0,
                brandId: 0,
            };
            axios
                .post('https://localhost:44352/api/Search', JSON.stringify(data), {
                    headers: {
                        Authorization: `Bearer ${cookies.access_token}`,
                        'content-type': 'application/json',
                    },
                    params: {
                        page: 1,
                        size: 1,
                    },
                })
                .then((res) => {
                    if (res.status === 200) {
                        if (res.data.productList.length) {
                            setListProduct(res.data.productList);
                            // setListProduct((prev) => [prev, ...res.data.productList]);
                            console.log(res.data.productList);
                        }
                    }
                })
                .catch((err) => console.log(err));
        } else if (params.get('category')) {
            const data = {
                keyWord: '',
                categoryId: params.get('category'),
                brandId: 0,
            };
            axios
                .post('https://localhost:44352/api/Search', JSON.stringify(data), {
                    headers: {
                        Authorization: `Bearer ${cookies.access_token}`,
                        'content-type': 'application/json',
                    },
                    params: {
                        page: 1,
                        size: 1,
                    },
                })
                .then((res) => {
                    if (res.status === 200) {
                        if (res.data.productList.length) {
                            setListProduct(res.data.productList);
                            // setListProduct((prev) => [prev, ...res.data.productList]);
                            console.log(res.data.productList);
                        }
                    }
                })
                .catch((err) => console.log(err));
        } else {
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
        }
    }, [cookies.access_token, params]);

    return (
        <div className={cx('wrap')}>
            <div className="container">
                <div>
                    <h1 className={cx('heading-1') + ' text-center'}>ALL POSTS</h1>

                    <div className={cx('control')}>
                        <div className={cx('ct-item')}>
                            <p>Place</p>
                            <Select
                                showSearch
                                style={{
                                    width: 200,
                                }}
                                placeholder="Select Place"
                                optionFilterProp="children"
                                filterOption={(input, option) => (option?.label ?? '').includes(input)}
                                filterSort={(optionA, optionB) =>
                                    (optionA?.label ?? '')
                                        .toLowerCase()
                                        .localeCompare((optionB?.label ?? '').toLowerCase())
                                }
                                options={[
                                    {
                                        value: '1',
                                        label: 'Cần Thơ',
                                    },
                                    {
                                        value: '2',
                                        label: 'TP. HCM',
                                    },
                                ]}
                            />
                        </div>
                        <div className={cx('price')}>
                            <p>Price range</p>
                            <div className="flex items-center">
                                <span style={{ width: '70px' }}>{NumberVND(priceValue[0])}đ</span>
                                <div className={cx('slider')}>
                                    <Slider
                                        value={priceValue}
                                        range
                                        step={100000}
                                        min={0}
                                        max={10000000}
                                        defaultValue={[0, 10000000]}
                                        onChange={(e) => setPriceValue(e)}
                                    />
                                </div>
                                <span style={{ width: '70px' }}>{NumberVND(priceValue[1])}đ</span>
                            </div>
                        </div>
                        <div className={cx('search')}>
                            <Input placeholder="Search here" />
                            <img src={searchIcon} alt="" />
                        </div>
                    </div>
                    <div className={cx('list')}>
                        <div className="grid grid-cols-4 gap-x-8 gap-y-12">
                            {listProduct.length ? (
                                listProduct?.map((product, index) => (
                                    <div key={index} className={cx('item')}>
                                        <CardNews product={product} />
                                    </div>
                                ))
                            ) : (
                                <h1 className="text-center">Không có sản phẩm nào</h1>
                            )}
                        </div>
                    </div>
                    <div className="flex justify-center">
                        <Pagination
                            onChange={(e) => console.log(e)}
                            size={1}
                            defaultCurrent={0}
                            total={listProduct.length}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default News;
