import classNames from 'classnames/bind';
import styles from './ButtonSubmit.module.scss';

const cx = classNames.bind(styles);

function ButtonSubmit({ children }) {
    return (
        <div className={cx('wrap')}>
            <div className={cx('button')}>{children}</div>
        </div>
    );
}

export default ButtonSubmit;
