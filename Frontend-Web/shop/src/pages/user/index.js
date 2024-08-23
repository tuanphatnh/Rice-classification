import classNames from 'classnames/bind';
import styles from './User.module.scss';
import React, { useEffect, useRef, useState } from 'react';

import { Avatar, Button, Rate } from 'antd';
import profileUser from '~/assets/icon/profile-user.png';
import axios from 'axios';
import { useParams, useSearchParams } from 'react-router-dom';
import { useCookies } from 'react-cookie';
import CardNews from '~/components/Global/CardNews';
const cx = classNames.bind(styles);

function User() {
    const [cookies, setCookies] = useCookies(['user']);
    const [params, setSearch] = useSearchParams();
    const [selectedImg, setSelectedImg] = useState(0);
    const [userInfo, setUserInfo] = useState({});

    const updateProfile = () => {};

    useEffect(() => {
        axios
            .get(`https://localhost:44352/api/User/getProfile`, {
                params: {
                    userId: params.get('id'),
                },
            })

            .then((res) => {
                setUserInfo(res.data);
                console.log(res.data);
            })
            .catch((err) => console.log(err));
    }, [params]);

    return (
        <div className={cx('wrap')}>
            <div className={cx('user')}>
                <div className={cx('avatar')}>
                    <img src={userInfo.avatarUrl ?? profileUser} alt="avatar" />
                </div>
                <div className={cx('info')}>
                    <h1 className="text-center">{userInfo.username}</h1>
                    <h2 className="text-center">{userInfo.email}</h2>

                    <Rate className="flex justify-center" disabled allowHalf defaultValue={2.5} />
                    <p className="px-2 text-center">2.5 Sao</p>
                    <div className="flex justify-center">
                        {cookies.user.id === userInfo.id && (
                            <Button className="mt-4 text-center" type="dashed" danger>
                                Chỉnh sửa thông tin
                            </Button>
                        )}
                    </div>
                </div>
            </div>
            <div className={cx('product')}>
                <h1 className='text-center'>Các tin đăng</h1>
                <div className={cx('list')}>
                    <div className="grid grid-cols-4 gap-x-8 gap-y-12">
                        {userInfo.products ? (
                            userInfo.products?.map((product, index) => (
                                <div key={index} className={cx('item')}>
                                    <CardNews product={product} />
                                </div>
                            ))
                        ) : (
                            <h1 className="text-center">Không có sản phẩm nào</h1>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default User;
