import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import styles from './MenuTool.module.scss';
import classNames from 'classnames/bind';

import { useState, useMemo, useEffect } from 'react';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Modal, Select, ColorPicker, Input, InputNumber, message } from 'antd';
import UploadImage from '../UploadImage';
import { useNavigate } from 'react-router-dom';

import axios from 'axios';
import { useSelector } from 'react-redux';
import { useCookies } from 'react-cookie';
import UploadVideo from '../UploadVideo';
import Loading from '../Loading';
import CreateAddress from '../CreateAddress';
const cx = classNames.bind(styles);

function MenuTool() {
    const [cookies, setCookie] = useCookies(['access_token'], ['user']);
    const navigate = useNavigate();

    const [listImage, setListImage] = useState([]);
    const [listVideo, setListVideo] = useState([]);
    const [isIconA, setIconA] = useState(false);
    const [open, setOpen] = useState(false);
    const [categorys, setCategorys] = useState([]);
    const [brands, setBrands] = useState([]);
    const [colors, setColors] = useState([]);
    const [sizes, setSizes] = useState([]);
    const [isLoader, setIsLoader] = useState(false);
    const [addressUser, setAddressUser] = useState(null);

    // form value
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [category, setCategory] = useState('');
    const [brand, setBrand] = useState('');
    const [color, setColor] = useState('');
    const [productName, setProductName] = useState('');
    const [size, setSize] = useState('');

    const { TextArea } = Input;

    useEffect(() => {
        // get api category
        axios
            .get(`https://localhost:44352/api/Category/getAll`)
            .then((res) => {
                if (res.status === 200) {
                    const newList = res.data.map(function (item) {
                        return {
                            value: item.id,
                            label: item.name,
                        };
                    });

                    setCategorys(newList);
                }
            })
            .catch((err) => console.log(err));

        // get api brand
        axios
            .get(`https://localhost:44352/api/Brand/getAll`)
            .then((res) => {
                if (res.status === 200) {
                    const newList = res.data.map(function (item) {
                        return {
                            value: item.id,
                            label: item.name,
                        };
                    });
                    setBrands(newList);
                }
            })
            .catch((err) => console.log(err));

        // get api colors
        axios
            .get(`https://localhost:44352/api/Color/getAll`)
            .then((res) => {
                if (res.status === 200) {
                    const newList = res.data.map(function (item) {
                        return {
                            value: item.id,
                            label: item.colorName,
                        };
                    });
                    setColors(newList);
                }
            })
            .catch((err) => console.log(err));

        // get api sizes
        axios
            .get(`https://localhost:44352/api/Size/getAll`)
            .then((res) => {
                if (res.status === 200) {
                    const newList = res.data.map(function (item) {
                        return {
                            value: item.id,
                            label: item.sizeName,
                        };
                    });
                    setSizes(newList);
                }
            })
            .catch((err) => console.log(err));

        // get address user
        axios
            .get(`https://localhost:44352/api/ShippingAddress/getUserAddress`, {
                headers: {
                    Authorization: `Bearer ${cookies.access_token}`,
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    const data = res.data.filter((address) => address.isDefault === true);
                    console.log(data);
                    setAddressUser(data[0]);
                }
            })
            .catch((err) => console.log(err));
    }, [cookies.access_token]);

    const submitForm = () => {
        if (validator()) {
            const data = {
                name: productName,
                description: description,
                stock: 0,
                price: price,
                status: 0,
                categoryId: category,
                brandId: brand,
                userId: cookies.user.id,
                sizeId: size,
                colorId: color,
                images: listImage,
            };

            setIsLoader(true);
            axios
                .post(`https://localhost:44352/api/Product/add`, JSON.stringify(data), {
                    headers: {
                        Authorization: `Bearer ${cookies.access_token}`,
                        'content-type': 'application/json',
                    },
                })
                .then((res) => {
                    if (res.status === 200) {
                        message.success('Thêm sản phẩm thành công!');
                        setIsLoader(false);
                        setOpen(false);
                    }
                })
                .catch((err) => console.log(err));
        }
    };

    const validator = () => {
        if (listImage.length < 2) {
            message.info('Tối thiểu 2 hình ảnh về sản phẩm!');
            return false;
        }

        if (listVideo.length < 1) {
            message.info('Tối thiểu 1 video về sản phẩm!');
            return false;
        }
        if (category === '') {
            message.info('Vui lòng chọn thể loại sản phẩm!');
            return false;
        }

        if (productName === '') {
            message.info('Vui lòng nhập tên sản phẩm!');
            return false;
        }

        if (size === '') {
            message.info('Vui lòng chọn size sản phẩm!');
            return false;
        }
        if (brand === '') {
            message.info('Vui lòng chọn hãng sản phẩm!');
            return false;
        }
        if (color === '') {
            message.info('Vui lòng chọn màu sắc sản phẩm!');
            return false;
        }
        if (price === '') {
            message.info('Vui lòng nhập giá bán sản phẩm!');
            return false;
        }

        return true;
    };

    const closeModal = () => {
        setIconA(false);
        setOpen(false);
    };
    const clickButton = () => {
        if (cookies.user !== undefined) {
            setIconA(!isIconA);
        } else {
            message.info('Bạn cần đăng nhập để được đăng tin!');
            navigate('/login');
        }
    };

    return (
        <div className={cx('wrap')}>
            {isLoader && <Loading />}
            <div className={cx('btn', isIconA && 'btn-close')} onClick={() => clickButton()}>
                <FontAwesomeIcon icon={faPlus} className={cx('icon', isIconA ? 'icon-in' : 'icon-out')} />
            </div>
            <div className={cx('list')}>
                <button className={cx('item', isIconA ? 'fade-in' : 'fade-out')} onClick={() => setOpen(true)}>
                    Đăng tin
                </button>
            </div>
            <Modal
                title="Đăng tin"
                centered
                open={open}
                onOk={() => submitForm()}
                onCancel={() => closeModal()}
                okButtonProps={{ type: 'default ' }}
                width={1200}
            >
                <div className={cx('box')}>
                    <div className={cx('upload')}>
                        <h1 className="text-center mb-5">
                            Hình ảnh sản phẩm <span>{'( Tối thiểu 2 hình )'}</span>
                        </h1>
                        <UploadImage listImage={listImage} setListImage={setListImage} />
                        <h1 className="text-center mb-5">
                            Video sản phẩm <span>{'( Tối thiểu 1 video )'}</span>
                        </h1>
                        <UploadVideo listVideo={listVideo} setListVideo={setListVideo} />
                    </div>
                    <div className={cx('content')}>
                        <div className="grid grid-cols-2">
                            <div>
                                <h3 className={cx('heading-1')}>Thông tin sản phẩm</h3>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Thể loại <span className="text-red-600">*</span>
                                    </h2>
                                    <Select
                                        showSearch
                                        style={{
                                            width: 300,
                                        }}
                                        placeholder="Chọn thể loại"
                                        optionFilterProp="children"
                                        filterOption={(input, option) => (option?.label ?? '').includes(input)}
                                        filterSort={(optionA, optionB) =>
                                            (optionA?.label ?? '')
                                                .toLowerCase()
                                                .localeCompare((optionB?.label ?? '').toLowerCase())
                                        }
                                        onChange={(e) => setCategory(e)}
                                        options={categorys}
                                    />
                                </div>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Tên sản phẩm <span className="text-red-600">*</span>
                                    </h2>
                                    <div style={{ width: '300px' }}>
                                        <TextArea
                                            showCount
                                            maxLength={100}
                                            style={{
                                                height: 60,
                                                marginBottom: 24,
                                            }}
                                            onChange={(e) => setProductName(e.target.value)}
                                            placeholder="..."
                                        />
                                    </div>
                                </div>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Mô tả <span className="text-red-600">*</span>
                                    </h2>
                                    <div style={{ width: '300px' }}>
                                        <TextArea
                                            showCount
                                            maxLength={1000}
                                            style={{
                                                height: 60,
                                                marginBottom: 24,
                                            }}
                                            onChange={(e) => setDescription(e.target.value)}
                                            placeholder="..."
                                        />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <h1 className={cx('heading-1')}>Thông tin chi tiết</h1>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Size <span className="text-red-600">*</span>
                                    </h2>
                                    <Select
                                        defaultValue=""
                                        style={{
                                            width: 300,
                                        }}
                                        onSelect={(e) => setSize(e)}
                                        options={sizes}
                                    />
                                </div>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Hãng <span className="text-red-600">*</span>
                                    </h2>
                                    <Select
                                        labelInValue="Chọn hãng"
                                        style={{
                                            width: 300,
                                        }}
                                        onSelect={(e) => setBrand(e.value)}
                                        options={brands}
                                    />
                                </div>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Màu sắc<span className="text-red-600">*</span>
                                    </h2>
                                    <Select
                                        labelInValue="Chọn màu"
                                        style={{
                                            width: 300,
                                        }}
                                        onSelect={(e) => setColor(e.value)}
                                        options={colors}
                                    />
                                </div>
                                <div className={cx('item')}>
                                    <h2 className={cx('heading-2')}>
                                        Giá bán <span className="text-red-600">*</span>
                                    </h2>
                                    <InputNumber
                                        style={{
                                            width: '300px',
                                        }}
                                        onChange={(e) => setPrice(e)}
                                        prefix="đ"
                                        suffix="Coin"
                                        type="number"
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default MenuTool;
