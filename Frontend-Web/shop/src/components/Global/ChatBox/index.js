import classNames from 'classnames/bind';
import styles from './ChatBox.module.scss';
import { useState } from 'react';
import chatIcon from '~/assets/icon/chat.png';
import { Modal } from 'antd';
import Chat from '../Chat';

const cx = classNames.bind(styles);

function ChatBox() {
    const [open, setOpen] = useState(false);
    return (
        <div className={cx('wrap')}>
            <div className={cx('box')} onClick={() => setOpen(true)}>
                <img src={chatIcon} alt="" />
                <span className={cx('quantity')}>12</span>
            </div>
            <Modal
                title="CHAT"
                centered
                open={open}
                onOk={() => setOpen(false)}
                onCancel={() => setOpen(false)}
                width={1200}
                footer={null}
            >
                <div className={cx('body')}>
                    <Chat />
                </div>
            </Modal>
        </div>
    );
}

export default ChatBox;
