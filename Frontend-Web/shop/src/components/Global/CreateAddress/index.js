import classNames from 'classnames/bind';
import styles from './CreateAddress.module.scss';
import { Button, Modal, Input, Select, message } from 'antd';
import { PhoneOutlined } from '@ant-design/icons';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { useCookies } from 'react-cookie';
import ButtonSubmit from '../ButtonSubmit';
import { faL } from '@fortawesome/free-solid-svg-icons';
const cx = classNames.bind(styles);

function CreateAddress({ setReload }) {
    const [cookies, setCookies] = useCookies(['user']);
    const [listProvince, setListProvince] = useState([]);
    const [listDistrict, setListDistricts] = useState([]);
    const [districts, setDistricts] = useState([]);
    const [listWard, setListWard] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [address, setAdress] = useState('');
    const [addressDetail, setAddressDetail] = useState('');
    const [phone, setPhone] = useState('');
    const [ward, setWard] = useState('');
    const { TextArea } = Input;

    useEffect(() => {
        // get api provinces
        axios
            .get(`https://provinces.open-api.vn/api/p/`)
            .then((res) => {
                let newList = res.data.map((item) => {
                    return {
                        value: item.code,
                        label: item.name,
                    };
                });
                setListProvince(newList);
            })
            .catch((err) => console.log(err));
    }, []);

    const submit = () => {
        if (validator()) {
            const addressData = {
                userId: cookies.user.id,
                addressName: address,
                phoneNumber: phone,
                wardId: ward,
                addressDetail: addressDetail,
            };

            createShippingAddress(addressData);
        }
    };

    const validator = () => {
        if (phone === '') {
            message.info('Vui lòng nhập số điện thoại');
            return false;
        }

        if (address === '') {
            message.info('Vui lòng nhập địa chỉ');
            return false;
        }

        if (addressDetail === '') {
            message.info('Vui chọn địa chỉ cụ thể');
            return false;
        }

        return true;
    };
    const getDistricts = (code) => {
        axios
            .get(`https://provinces.open-api.vn/api/p/${code}?depth=3`)
            .then((res) => {
                setDistricts(res.data.districts);
                setListDistricts(
                    res.data.districts.map((districts) => {
                        return {
                            value: districts.code,
                            label: districts.name,
                        };
                    }),
                );
            })
            .catch((err) => console.log(err));
    };

    const getWards = (code) => {
        const district = districts.filter((dist) => dist.code === code);
        setListWard(
            district[0].wards.map((ward) => {
                return {
                    value: ward.code,
                    label: ward.name,
                };
            }),
        );
    };

    const createShippingAddress = (data) => {
        axios
            .post(`https://localhost:44352/api/ShippingAddress/create`, JSON.stringify(data), {
                headers: {
                    Authorization: `Bearer ${cookies.access_token}`,
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    message.success('Đã thêm địa chỉ');
                    setIsModalOpen(false);
                    setReload((state) => !state);
                }
            })
            .catch((err) => console.log(err));
    };
    return (
        <div>
            <Button type="default" onClick={() => setIsModalOpen(true)}>
                Thêm dịa chỉ mới
            </Button>
            <Modal
                title="Thêm địa chỉ"
                width={700}
                open={isModalOpen}
                onOk={() => submit()}
                onCancel={() => setIsModalOpen(false)}
                okButtonProps={{ type: 'default ' }}
            >
                <div className={cx('box')}>
                    <div className={cx('content')}>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>
                                Số điện thoại <span className="text-red-600">*</span>
                            </h2>
                            <Input
                                type="tel"
                                style={{ width: '300px' }}
                                placeholder="..."
                                prefix={<PhoneOutlined />}
                                onChange={(value) => setPhone(value.target.value)}
                            />
                        </div>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>
                                Tên địa chỉ <span className="text-red-600">*</span>
                            </h2>
                            <div style={{ width: '300px' }}>
                                <Input
                                    showCount
                                    maxLength={100}
                                    onChange={(e) => setAdress(e.target.value)}
                                    placeholder="..."
                                />
                            </div>
                        </div>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>
                                Địa chỉ <span className="text-red-600">*</span>
                            </h2>

                            <Select
                                showSearch
                                style={{
                                    width: 300,
                                }}
                                placeholder="Tỉnh"
                                optionFilterProp="children"
                                filterOption={(input, option) => (option?.label ?? '').includes(input)}
                                filterSort={(optionA, optionB) =>
                                    (optionA?.label ?? '    ')
                                        .toLowerCase()
                                        .localeCompare((optionB?.label ?? '').toLowerCase())
                                }
                                onSelect={(value, item) => {
                                    setListDistricts([]);
                                    getDistricts(value);
                                }}
                                options={listProvince}
                            />
                        </div>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>
                                {/* Địa chỉ - Tỉnh <span className="text-red-600">*</span> */}
                            </h2>
                            <Select
                                showSearch
                                style={{
                                    width: 300,
                                }}
                                placeholder="Huyện/Quận"
                                optionFilterProp="children"
                                filterOption={(input, option) => (option?.label ?? '').includes(input)}
                                filterSort={(optionA, optionB) =>
                                    (optionA?.label ?? '    ')
                                        .toLowerCase()
                                        .localeCompare((optionB?.label ?? '').toLowerCase())
                                }
                                onSelect={(value, item) => {
                                    console.log(value);
                                    getWards(value);
                                    setAddressDetail(item.label);
                                }}
                                options={listDistrict}
                            />
                        </div>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>
                                {/* Địa chỉ - Tỉnh <span className="text-red-600">*</span> */}
                            </h2>
                            <Select
                                showSearch
                                style={{
                                    width: 300,
                                }}
                                placeholder="Xã - phường - thị trấn"
                                optionFilterProp="children"
                                filterOption={(input, option) => (option?.label ?? '').includes(input)}
                                filterSort={(optionA, optionB) =>
                                    (optionA?.label ?? '    ')
                                        .toLowerCase()
                                        .localeCompare((optionB?.label ?? '').toLowerCase())
                                }
                                onSelect={(value, item) => {
                                    setWard(value);
                                }}
                                options={listWard}
                            />
                        </div>
                        <div className={cx('item')}>
                            <h2 className={cx('heading-2')}>Địa chỉ cụ thể</h2>
                            <div style={{ width: '300px' }}>
                                <TextArea
                                    showCount
                                    maxLength={1000}
                                    style={{
                                        height: 60,
                                        marginBottom: 24,
                                    }}
                                    onChange={(e) => setAddressDetail(e.target.value)}
                                    placeholder="..."
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default CreateAddress;
