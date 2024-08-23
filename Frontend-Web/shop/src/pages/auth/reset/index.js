import classNames from 'classnames/bind';
import styles from './Login.module.scss';

import { useEffect, useRef, useState } from 'react';
import { Link, useNavigate, useParams, useSearchParams } from 'react-router-dom';
import axios from 'axios';
import { message } from 'antd';
import Loading from '~/components/Global/Loading';
import { EyeOutlined, EyeInvisibleOutlined } from '@ant-design/icons';

const cx = classNames.bind(styles);

function ResetPassword() {
    const navigate = useNavigate();
    const [params] = useSearchParams();
    const [isLoader, setIsLoader] = useState(false);
    const passwordRef = useRef(null);
    const confirmPWRef = useRef(null);
    const [password, setPassword] = useState('');
    const [confirmPW, setConfirmPW] = useState('');
    const logPassword = useRef(null);
    const logConfirmPw = useRef(null);
    const [isPassword, setIsPw] = useState(false);
    const [isConfirmPw, setIsConfirmPw] = useState(false);

    const submitLogin = () => {
        if (validator()) {
            setIsLoader(true);
            const data = {
                token: params.get('token'),
                email: params.get('email'),
                newPassword: password,
                confirmPassword: confirmPW,
            };
            console.log(data);
            axios
                .post(`https://localhost:44352/api/Auth/resetPassword`, data)
                .then((res) => {
                    if (res.status === 200) {
                        message.success('Password has been reset successfully!');
                        navigate('/login');
                        setIsLoader(false);
                    }
                })
                .catch((err) => {
                    console.log(err);
                    message.error('Failed');
                    setIsLoader(false);
                });
        }
    };

    const validator = () => {
        if (!password) {
            passwordRef.current.focus();
            logPassword.current.style.display = 'inline';
            return false;
        } else {
            logPassword.current.style.display = 'none';
        }

        if (password.length < 6) {
            passwordRef.current.focus();
            logPassword.current.style.display = 'inline';
            logPassword.current.innerHTML = 'Your password is shorter than 6 characters';
            return false;
        } else {
            logPassword.current.style.display = 'none';
        }

        if (confirmPW !== password) {
            confirmPWRef.current.focus();
            logConfirmPw.current.style.display = 'inline';
            return false;
        } else {
            logConfirmPw.current.style.display = 'none';
        }
        return true;
    };

    return (
        <div className={cx('wrap')}>
            <div className={cx('box')}>
                <h1 className="text-center text-4xl">Reset Password</h1>
                <div className={cx('group')}>
                    <div className={cx('item', 'password')}>
                        <input
                            className={cx('input-password')}
                            ref={passwordRef}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            type={isPassword ? 'text' : 'password'}
                            onKeyDown={(e) => e.key === 'Enter' && submitLogin()}
                            placeholder="New password"
                        />
                        <div className={cx('icon')}>
                            {isPassword ? (
                                <EyeOutlined onClick={() => setIsPw(false)} />
                            ) : (
                                <EyeInvisibleOutlined onClick={() => setIsPw(true)} />
                            )}
                        </div>
                        <span ref={logPassword} className={cx('item-log')} style={{ display: 'none' }}>
                            Please enter a password
                        </span>
                    </div>
                    <div className={cx('item', 'password')}>
                        <input
                            className={cx('input-password')}
                            ref={confirmPWRef}
                            value={confirmPW}
                            onChange={(e) => setConfirmPW(e.target.value)}
                            type={isConfirmPw ? 'text' : 'password'}
                            onKeyDown={(e) => e.key === 'Enter' && submitLogin()}
                            placeholder="Confirm new password"
                        />
                        <div className={cx('icon')}>
                            {isConfirmPw ? (
                                <EyeOutlined onClick={() => setIsConfirmPw(false)} />
                            ) : (
                                <EyeInvisibleOutlined onClick={() => setIsConfirmPw(true)} />
                            )}
                        </div>
                        <span ref={logConfirmPw} className={cx('item-log')} style={{ display: 'none' }}>
                            Password confirm does not match
                        </span>
                    </div>
                    <div className={cx('item')}>
                        <button className={cx('btn')} onClick={() => submitLogin()}>
                            <span>Reset</span>
                        </button>
                    </div>
                    <div className={cx('ruler-bar')}>
                        <div className={cx('ruler', 'left')}></div>
                        <span>Or</span>
                        <div className={cx('ruler', 'right')}></div>
                    </div>
                    <div className={cx('register') + ' mt-5'}>
                        <h4>
                            Already have an account?
                            <Link to="/login">
                                <span className="ml-3">Login now</span>
                            </Link>
                        </h4>
                    </div>
                </div>
            </div>
            {isLoader && <Loading />}
        </div>
    );
}

export default ResetPassword;
