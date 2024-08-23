import styles from './Loading.module.scss';
import classNames from 'classnames/bind';
import { LoadingOutlined } from '@ant-design/icons';
import { Spin } from 'antd';
const antIcon = (
    <LoadingOutlined
        style={{
            fontSize: 50,
            color: '#fff',
        }}
        spin
    />
);
const cx = classNames.bind(styles);

function Loading() {
    return (
        <div className={cx('wrap')}>
            <Spin indicator={antIcon} />
        </div>
    );
}

export default Loading;
