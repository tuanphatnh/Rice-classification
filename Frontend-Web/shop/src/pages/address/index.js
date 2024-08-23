import classNames from 'classnames/bind';
import styles from './Address.module.scss';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { useCookies } from 'react-cookie';
import { Button, Modal, Popconfirm, message } from 'antd';
import { getWard } from '~/functions';
import { list } from 'postcss';
import { faL } from '@fortawesome/free-solid-svg-icons';
import CreateAddress from '~/components/Global/CreateAddress';

const cx = classNames.bind(styles);

function Address() {
    const [cookies, setCookies] = useCookies(['user']);
    const [listAdd, setListAdd] = useState([]);
    const [isModalDetail, setModalDetail] = useState(false);
    const [ward, setWard] = useState(null);
    const [district, setDistrict] = useState(null);
    const [province, setProvince] = useState(null);
    const [address, setAddress] = useState(null);
    const [isReset, setReaload] = useState(false);

    useEffect(() => {
        axios
            .get(`https://localhost:44352/api/ShippingAddress/getUserAddress`, {
                headers: {
                    Authorization: `Bearer ${cookies.access_token}`,
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    setListAdd(res.data);
                }
            })
            .catch((err) => console.log(err));
    }, [cookies.access_token, isReset]);

    const openModalDetail = (address) => {
        setModalDetail(true);
        setAddress(address);

        axios
            .get(`https://provinces.open-api.vn/api/w/${address.wardId}`)
            .then((res) => {
                if (res.status === 200) {
                    setWard(res.data);
                }
            })
            .catch((err) => {
                console.log(err);
            });
    };

    const closeModal = () => {
        setAddress(null);
        setDistrict(null);
        setWard(null);
        setProvince(null);
        setModalDetail(false);
    };

    useEffect(() => {
        if (ward?.district_code) {
            axios
                .get(`https://provinces.open-api.vn/api/d/${ward.district_code}`)
                .then((res) => {
                    if (res.status === 200) {
                        setDistrict(res.data);
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    }, [ward]);

    useEffect(() => {
        if (district?.province_code) {
            axios
                .get(`https://provinces.open-api.vn/api/p/${district.province_code}`)
                .then((res) => {
                    if (res.status === 200) {
                        setProvince(res.data);
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    }, [district]);

    const confirm = (id) => {
        axios
            .delete(`https://localhost:44352/api/ShippingAddress/delete/${id}`, {
                headers: {
                    Authorization: `Bearer ${cookies.access_token}`,
                    'content-type': 'application/json',
                },
            })
            .then((res) => {
                if (res.status === 200) {
                    message.success('Xóa thành công!');
                    setReaload(!isReset);
                }
            })
            .catch((err) => {
                message.err('Xóa thất bại!');
            });
    };
    const cancel = (e) => {
        message.error('Click on No');
    };

    return (
        <div className={cx('wrap')}>
            <div className={cx('container')}>
                <div className={cx('header')}>
                    <h1>Địa chỉ của tôi</h1>
                    <CreateAddress setReload={setReaload} />
                </div>
                <div className={cx('content')}>
                    {listAdd.length ? (
                        listAdd.map((address, index) => (
                            <div key={index} className={cx('item')}>
                                <div className="flex justify-between">
                                    <div className={cx('info')}>
                                        <h2 className="text-xxl font-semibold mb-2">{address?.addressName}</h2>
                                        <p className="pl-5 pb-2 text-xl">
                                            <strong>{address?.phoneNumber}</strong>
                                        </p>
                                        <p className="pl-5 pb-2 text-xl">{address?.addressDetail}</p>
                                        <Button type="dashed" className="mx-2" onClick={() => openModalDetail(address)}>
                                            Chi Tiết
                                        </Button>
                                    </div>
                                    <div className={cx('edit')}>
                                        <div>
                                            <Button type="dashed" className="mx-2">
                                                Cập nhật
                                            </Button>

                                            <Popconfirm
                                                title="Xóa địa chỉ"
                                                description="Bạn có chắc muốn xóa chứ?"
                                                onConfirm={() => confirm(address.id)}
                                                onCancel={cancel}
                                                okText="Yes"
                                                cancelText="No"
                                            >
                                                <Button danger>Xóa</Button>
                                            </Popconfirm>
                                        </div>
                                        {address.isDefault ? (
                                            <Button type="primary" danger>
                                                Mặt định
                                            </Button>
                                        ) : (
                                            <Button type="default">Chọn làm mặt định</Button>
                                        )}
                                    </div>
                                </div>
                            </div>
                        ))
                    ) : (
                        <div className={cx('item')}>
                            <h1>Chưa có địa chỉ nào hãy thêm địa chỉ mới</h1>
                        </div>
                    )}
                </div>
            </div>
            <Modal title="Địa chỉ" open={isModalDetail} onOk={() => closeModal()} onCancel={() => closeModal()}>
                <h2 className="text-xxl font-semibold mb-2">{address?.addressName}</h2>
                <p className="pl-5 pb-2 text-xl">
                    <strong>{address?.phoneNumber}</strong>
                </p>
                <p className="pl-5 pb-2 text-xl">{ward && `${ward?.name}, ${district?.name}, ${province?.name}`}</p>
                <p className="pl-5 pb-2 text-xl">{address?.addressDetail}</p>
            </Modal>
        </div>
    );
}

export default Address;
