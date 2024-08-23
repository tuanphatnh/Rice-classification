import classNames from 'classnames/bind';
import styles from './Chat.module.scss';
import { ChatList, Avatar } from 'react-chat-elements';
import { SendOutlined } from '@ant-design/icons';
import 'react-chat-elements/dist/main.css';

import avatar from '~/assets/images/Avatar_FB.jpg';
import ChatItem from '../ChatItem';
import { useEffect, useRef, useState } from 'react';

const cx = classNames.bind(styles);

function Chat() {
    const [inputChat, setInputChat] = useState('');
    const [messsageList, setMessageList] = useState([
        {
            avatar: avatar,
            text: 'Hello',
            time: 'T6 12:12',
            position: 'left',
        },
    ]);
    const inputRef = useRef(null);
    const messagesEndRef = useRef(null);

    useEffect(() => {
        inputRef.current.focus();
    }, []);

    const scrollToBottom = () => {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    };

    useEffect(() => {
        scrollToBottom();
    }, [messsageList]);

    const addChat = () => {
        if (inputChat) {
            const itemChat = {
                avatar: avatar,
                text: inputChat,
                time: 'T6 12:12',
                position: 'right',
            };

            setMessageList((prev) => [...prev, itemChat]);

            setInputChat('');
            inputRef.current.focus();
        }
    };

    return (
        <div className={cx('wrap')}>
            <div className="grid grid-cols-4">
                <div className={cx('left')}>
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                    <ChatList
                        className="chat-list"
                        dataSource={[
                            {
                                avatar: 'https://avatars.githubusercontent.com/u/80540635?v=4',
                                alt: 'kursat_avatar',
                                title: 'Kursat',
                                subtitle: "Why don't we go to the No Way Home movie this weekend ?",
                                date: new Date(),
                                unread: 3,
                            },
                        ]}
                    />
                </div>
                <div className={cx('right') + ' col-span-3'}>
                    <div className={cx('head')}>
                        <Avatar src={avatar} alt="avatar" size="xlarge" type="circle" />
                        <h2>Chung Phát Tiến</h2>
                    </div>
                    <div className={cx('content')}>
                        {messsageList.map((chat) => (
                            <ChatItem position={chat.position} avatar={chat.avatar} text={chat.text} time={chat.time} />
                        ))}
                        <div ref={messagesEndRef} />
                    </div>
                    <div className={cx('footer')}>
                        <div className={cx('chat-footer')}>
                            <img className={cx('user-avatar')} src={avatar} alt="" />
                            <div className={cx('chat-input')}>
                                <input
                                    value={inputChat}
                                    onChange={(e) => setInputChat(e.target.value)}
                                    ref={inputRef}
                                    type="text"
                                    onKeyDown={(e) => {
                                        if (e.key === 'Enter') {
                                            addChat();
                                        }
                                    }}
                                    placeholder="Bạn muốn nói gì đó"
                                />
                                <span />
                            </div>
                            <div className={cx('btn-send')} onClick={() => addChat()}>
                                <SendOutlined />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Chat;
