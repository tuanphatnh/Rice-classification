import classNames from 'classnames/bind';
import styles from './ChatItem.module.scss';
import { Avatar } from 'antd';

const cx = classNames.bind(styles);

function ChatItem({ position, text, avatar, time }) {
    return (
        <div className={cx('wrap')}>
            {position === 'left' ? (
                <div className="flex items-center">
                    <Avatar src={avatar} />
                    <div className={cx('content')} style={{ backgroundColor: '#e3e1e1' }}>
                        <span>{text}</span>
                    </div>
                    <span>{time}</span>
                </div>
            ) : (
                <div className="flex items-center justify-end">
                    <span>{time}</span>
                    <div className={cx('content')} style={{ backgroundColor: '#0084ff', color: '#fff' }}>
                        <span>{text}</span>
                    </div>
                    <Avatar src={avatar} />
                </div>
            )}
        </div>
    );
}

export default ChatItem;
