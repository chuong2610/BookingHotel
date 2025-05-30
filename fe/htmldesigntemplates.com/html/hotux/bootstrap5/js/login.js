// API endpoints
const API_URL = 'http://localhost:5178/api';


    
    async function handleLogin(event) {
        event.preventDefault();
        const email = $('#loginEmail').val();
        const password = $('#loginPassword').val();
        
        console.log('Login attempt with:', { email, password });
        
        // Gọi API đăng nhập
        $.ajax({
            url: `${API_URL}/Auth/login`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ email, password }),
            success: function(response) {
                console.log('Login successful:', response);
                localStorage.setItem('token', response);
                // localStorage.setItem('user', JSON.stringify(response.user));
                updateHeaderUI(true);
                window.location.href = 'index.html';
            },
            error: function(xhr, status, error) {
                console.error('Login failed:', error);
                alert(xhr.responseJSON?.message || 'Đăng nhập thất bại');
            }
        });
    }


// Hàm xử lý đăng nhập từ header
async function handleHeaderLogin(event) {
    event.preventDefault();
    
    const email = document.getElementById('headerEmail').value;
    const password = document.getElementById('headerPassword').value;

    console.log('Attempting header login with:', { email, password });

    try {
        const response = await fetch(`${API_URL}/Auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password })
        });

        console.log('Header login response status:', response.status);
        const data = await response.json();
        console.log('Header login response data:', data);

        if (response.ok) {
            // Lưu token vào localStorage
            localStorage.setItem('token', data.token);
            localStorage.setItem('user', JSON.stringify(data.user));
            
            // Cập nhật UI header
            updateHeaderUI(true, data.user);
            
            // Đóng modal đăng nhập nếu có
            const loginModal = document.getElementById('loginModal');
            if (loginModal) {
                loginModal.style.display = 'none';
            }
        } else {
            console.error('Header login failed:', data.message);
            alert(data.message || 'Đăng nhập thất bại');
        }
    } catch (error) {
        console.error('Header login error:', error);
        alert('Có lỗi xảy ra khi đăng nhập');
    }
}

// Hàm xử lý đăng ký
async function handleRegister(event) {
    event.preventDefault();
    
    const name = document.getElementById('registerName').value;
    const email = document.getElementById('registerEmail').value;
    const password = document.getElementById('registerPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (password !== confirmPassword) {
        alert('Mật khẩu xác nhận không khớp');
        return;
    }

    try {
        const response = await fetch(`${API_URL}/auth/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ name, email, password })
        });

        const data = await response.json();

        if (response.ok) {
            // Hiển thị thông báo xác thực email
            showVerificationMessage(email);
        } else {
            alert(data.message || 'Đăng ký thất bại');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Có lỗi xảy ra khi đăng ký');
    }
}

// Hàm hiển thị thông báo xác thực email
function showVerificationMessage(email) {
    const verificationDiv = document.getElementById('verificationMessage');
    if (verificationDiv) {
        verificationDiv.innerHTML = `
            <div class="alert alert-info">
                <h4>Vui lòng xác thực email của bạn</h4>
                <p>Chúng tôi đã gửi một email xác thực đến ${email}</p>
                <p>Vui lòng kiểm tra hộp thư của bạn và nhấp vào liên kết xác thực để hoàn tất quá trình đăng ký.</p>
                <p>Nếu bạn không nhận được email, vui lòng kiểm tra thư mục spam hoặc <a href="#" onclick="resendVerificationEmail('${email}')">gửi lại email xác thực</a>.</p>
            </div>
        `;
        verificationDiv.style.display = 'block';
    }
}

// Hàm gửi lại email xác thực
// async function resendVerificationEmail(email) {
//     try {
//         const response = await fetch(`${API_URL}/auth/resend-verification`, {
//             method: 'POST',
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify({ email })
//         });

//         const data = await response.json();

//         if (response.ok) {
//             alert('Email xác thực đã được gửi lại. Vui lòng kiểm tra hộp thư của bạn.');
//         } else {
//             alert(data.message || 'Không thể gửi lại email xác thực');
//         }
//     } catch (error) {
//         console.error('Error:', error);
//         alert('Có lỗi xảy ra khi gửi lại email xác thực');
//     }
// }

// Hàm xác thực email
// async function verifyEmail(token) {
//     try {
//         const response = await fetch(`${API_URL}/auth/verify-email`, {
//             method: 'POST',
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify({ token })
//         });

//         const data = await response.json();

//         if (response.ok) {
//             alert('Email đã được xác thực thành công! Bạn có thể đăng nhập ngay bây giờ.');
//             window.location.href = '/login.html';
//         } else {
//             alert(data.message || 'Xác thực email thất bại');
//         }
//     } catch (error) {
//         console.error('Error:', error);
//         alert('Có lỗi xảy ra khi xác thực email');
//     }
// }

// Hàm cập nhật UI header dựa trên trạng thái đăng nhập
function updateHeaderUI(isLoggedIn, user = null) {
    const loginBtn = document.getElementById('headerLoginBtn');
    const registerBtn = document.getElementById('headerRegisterBtn');
    const userMenu = document.getElementById('userMenu');
    const userName = document.getElementById('userName');

    if (isLoggedIn ) {
        // Ẩn nút đăng nhập/đăng ký
        if (loginBtn) loginBtn.style.display = 'none';
        if (registerBtn) registerBtn.style.display = 'none';
        
        // Hiển thị menu người dùng
        if (userMenu) userMenu.style.display = 'block';
        if (userName) userName.textContent = user.name;
    } else {
        // Hiển thị nút đăng nhập/đăng ký
        if (loginBtn) loginBtn.style.display = 'block';
        if (registerBtn) registerBtn.style.display = 'block';
        
        // Ẩn menu người dùng
        if (userMenu) userMenu.style.display = 'none';
    }
}

// Hàm đăng xuất
function handleLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    updateHeaderUI(false);
    window.location.reload();
}

// Hàm kiểm tra trạng thái đăng nhập khi load trang
function checkAuthStatus() {
    const token = localStorage.getItem('token');
    const user = JSON.parse(localStorage.getItem('user') || 'null');
    
    if (token && user) {
        updateHeaderUI(true, user);
    } else {
        updateHeaderUI(false);
    }
}

// Thêm event listeners khi trang được load
document.addEventListener('DOMContentLoaded', () => {
    console.log('DOM loaded, setting up event listeners');
    
    const loginForm = document.getElementById('loginForm');
    const headerLoginForm = document.getElementById('headerLoginForm');
    const registerForm = document.getElementById('registerForm');
    const logoutBtn = document.getElementById('logoutBtn');

    console.log('Found elements:', {
        loginForm: !!loginForm,
        headerLoginForm: !!headerLoginForm,
        registerForm: !!registerForm,
        logoutBtn: !!logoutBtn
    });

    if (loginForm) {
        console.log('Adding submit listener to login form');
        loginForm.addEventListener('submit', handleLogin);
    }

    if (headerLoginForm) {
        console.log('Adding submit listener to header login form');
        headerLoginForm.addEventListener('submit', handleHeaderLogin);
    }

    if (registerForm) {
        console.log('Adding submit listener to register form');
        registerForm.addEventListener('submit', handleRegister);
    }

    if (logoutBtn) {
        console.log('Adding click listener to logout button');
        logoutBtn.addEventListener('click', handleLogout);
    }

    // Kiểm tra token xác thực email trong URL
    // const urlParams = new URLSearchParams(window.location.search);
    // const verificationToken = urlParams.get('token');
    // if (verificationToken) {
    //     verifyEmail(verificationToken);
    // }

    // Kiểm tra trạng thái đăng nhập
    checkAuthStatus();
}); 


