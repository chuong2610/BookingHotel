// Function to load header
async function loadHeader() {
    try {
        const response = await fetch('header.html');
        const html = await response.text();
        document.getElementById('header-container').innerHTML = html;
    } catch (error) {
        console.error('Error loading header:', error);
    }
}

// Function to load footer
async function loadFooter() {
    try {
        const response = await fetch('footer.html');
        const html = await response.text();
        document.getElementById('footer-container').innerHTML = html;
    } catch (error) {
        console.error('Error loading footer:', error);
    }
}

// Load components when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    loadHeader();
    loadFooter();
}); 