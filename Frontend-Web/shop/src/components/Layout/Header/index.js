import classNames from 'classnames/bind';
import styles from './Header.module.scss';

import icon from '~/assets/images/icon_shopping.png';
import Search from '../../HomePage/Search';
import UserNav from '~/components/HomePage/UserNav';
import { Link } from 'react-router-dom';
import MenuTool from '~/components/Global/MenuTool';
import { useCookies } from 'react-cookie';

const cx = classNames.bind(styles);

function Header() {
    const [cookies, setCookies] = useCookies(['user']);
    return (
        <div className={cx('wrap')}>
            <Link to="/home">
                <img className={cx('icon')} placeholder="Bạn muốn tìm gì ..." src={icon} alt="" />
            </Link>
            <div className={cx('search')}>
                <Search />
            </div>
            <div>
                <UserNav />
            </div>
            {cookies.user && <MenuTool />}
        </div>
    );
}

export default Header;
