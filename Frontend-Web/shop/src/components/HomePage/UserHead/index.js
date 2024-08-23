import classNames from 'classnames/bind';
import styles from './UserHead.module.scss';
import { Avatar, Rate } from 'antd';

import profileUser from '~/assets/icon/profile-user.png';
import userIcon from '~/assets/icon/user.png';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { useCookies } from 'react-cookie';

const cx = classNames.bind(styles);

function UserHead() {
    const [cookies, setCookies] = useCookies(['user']);

    return (
        <div className={cx('wrap')}>
            {cookies.user ? (
                <Link to={`/user?id=${cookies.user.id}`}>
                    <div className={cx('user')}>
                        <Avatar size={40} src={<img src={cookies.user.avatarUrl ?? profileUser} alt="avatar" />} />
                        <div className={cx('infor')}>
                            <h1>{cookies?.user.username}</h1>
                            <div>
                                <Rate className={cx('rate')} disabled allowHalf defaultValue={2.5} />
                                <span className="px-2">2.5</span>
                            </div>
                        </div>
                        <div className={cx('coin')}>
                            <span>{cookies.user?.userBalance} Coin</span>
                        </div>
                    </div>
                </Link>
            ) : (
                <div className={cx('user')}>
                    <Avatar size={40} src={<img src={userIcon} alt="avatar" />} />
                    <Link to={'/login'}>
                        <h1 className={cx('heading')}>Đăng nhập</h1>
                    </Link>
                </div>
            )}
        </div>
    );
}

export default UserHead;
